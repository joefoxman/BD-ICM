using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace Bd.Icm
{
    [Serializable]
    public class ExcelExporter 
    {
        #region [Private Methods]

        private const string ColLevel = "Level";
        private const string ColInstrumentName = "Instrument Name";
        private const string ColInstrumentSerialNo = "Instrument Serial #";
        private const string ColPartId = "Part ID";
        private const string ColPartName = "Part Name";
        private const string ColDescription = "Description";
        private const string ColSapPartNo = "SAP Part #";
        private const string ColSapPartType = "SAP Part Type";
        private const string ColDocumentNo = "Document #";
        private const string ColDashNo = "Dash #";
        private const string ColRevisionNo = "Revision #";
        private const string ColManufacturer = "Manufacturer";
        private const string ColMfrPartNo = "Mfr. Part #";
        private const string ColLotCode = "Lot Code";
        private const string ColDateCode = "Date Code";
        private const string ColParameter = "Parameter";
        private const string ColParameterValue = "Parameter Value";
        private const string ColModifedDate = "Modified Date";
        private const string ColModifedByUser = "Modified By User";
        private const string ColPartDescription = "Part Description";
        private const string ColAction = "Action";
        private const string ColActionDate = "Action Date";

        private static DataTable CreateDataTableParts()
        {
            var dataTableParts = new DataTable("tblDataParts");
            dataTableParts.Columns.AddRange(new[] {
                    new DataColumn(ColLevel, typeof (int)),
                    new DataColumn(ColInstrumentName, typeof (string)),
                    new DataColumn(ColInstrumentSerialNo, typeof (string)),
                    new DataColumn(ColPartId, typeof (int)),
                    new DataColumn(ColPartName, typeof (string)),
                    new DataColumn(ColDescription, typeof (string)),
                    new DataColumn(ColSapPartNo, typeof (string)),
                    new DataColumn(ColSapPartType, typeof (string)),
                    new DataColumn(ColDocumentNo, typeof (string)),
                    new DataColumn(ColDashNo, typeof (string)),
                    new DataColumn(ColRevisionNo, typeof (string)),
                    new DataColumn(ColManufacturer, typeof (string)),
                    new DataColumn(ColMfrPartNo, typeof (string)),
                    new DataColumn(ColLotCode, typeof (string)),
                    new DataColumn(ColDateCode, typeof (string)),
                    new DataColumn(ColParameter, typeof (string)),
                    new DataColumn(ColParameterValue, typeof (string)),
                    new DataColumn(ColModifedDate, typeof (string)),
                    new DataColumn(ColModifedByUser, typeof (string)),
                });
            return dataTableParts;
        }

        private static DataTable CreateDataTableActions()
        {
            var dataTableActions = new DataTable("tblDataActions");
            dataTableActions.Columns.AddRange(new[] {
                    new DataColumn(ColPartName, typeof (string)),
                    new DataColumn(ColPartDescription, typeof (string)),
                    new DataColumn(ColAction, typeof (string)),
                    new DataColumn(ColDescription, typeof (string)),
                    new DataColumn(ColActionDate, typeof (string)),
                    new DataColumn(ColModifedDate, typeof (string)),
                    new DataColumn(ColModifedByUser, typeof (string))
                });
            return dataTableActions;
        }

        private static DataTable CreateDataTableSettings()
        {
            var dataTableSettings = new DataTable("tblDataSettings");
            dataTableSettings.Columns.AddRange(new[] {
                    new DataColumn(ColPartName, typeof (string)),
                    new DataColumn(ColPartDescription, typeof (string)),
                    new DataColumn(ColParameter, typeof (string)),
                    new DataColumn(ColParameterValue, typeof (string)),
                    new DataColumn(ColModifedDate, typeof (string)),
                    new DataColumn(ColModifedByUser, typeof (string))
                });
            return dataTableSettings;
        }

        private static DataTable AddPartActions (
            DataTable dataTableActions,
            Part part) { 
            foreach (var action in part.Actions) {
                var newRow = dataTableActions.NewRow();
                newRow[ColPartName] = part.Name;
                newRow[ColPartDescription] = part.Description;
                newRow[ColAction] = action.Action;
                newRow[ColDescription] = action.Description;
                newRow[ColActionDate] = action.ActionDate;
                newRow[ColModifedDate] = action.ModifiedDate;
                newRow[ColModifedByUser] = "";
                if (action.Modifier != null) {
                    newRow[6] = $"{action.Modifier.FirstName} {action.Modifier.LastName}";
                }
                dataTableActions.Rows.Add(newRow);
            }
            return dataTableActions;
        }

        private static DataTable AddPartSettings(
           DataTable dataTableParts,
           Part part) {
            var dataTableSettings = CreateDataTableSettings();

            foreach (var setting in part.Metadata) {
                var newRow = dataTableSettings.NewRow();
                newRow[ColPartName] = part.Name;
                newRow[ColPartDescription] = part.Description;
                newRow[ColParameter] = setting.MetaKey;
                newRow[ColParameterValue] = setting.MetaValue;
                newRow[ColModifedDate] = setting.ModifiedDate;
                newRow[ColModifedByUser] = "";
                if (setting.Modifier != null) {
                    newRow[5] = $"{setting.Modifier.FirstName} {setting.Modifier.LastName}";
                }
                dataTableSettings.Rows.Add(newRow);
            }
            foreach (DataRow settingRow in dataTableSettings.Rows) {
                var settingNewRow = dataTableParts.NewRow();
                settingNewRow[ColParameter] = settingRow[2];
                settingNewRow[ColParameterValue] = settingRow[3];
                settingNewRow[ColModifedDate] = settingRow[4];
                settingNewRow[ColModifedByUser] = settingRow[5];
                dataTableParts.Rows.Add(settingNewRow);
            }

           
            return dataTableParts;
           }

        #endregion

        #region [Factory Methods]

        public static MemoryStream CreateInstrumentExport(IEnumerable<Instrument> instruments)
        {
            instruments.ThrowIfNull(nameof(instruments));
            var package = new ExcelPackage();

            foreach (var instrument in instruments) {
                var dataTableParts = CreateDataTableParts();
                var row = dataTableParts.NewRow();
                row[ColInstrumentName] = instrument.NickName;
                row[ColInstrumentSerialNo] = instrument.SerialNumber;
                dataTableParts.Rows.Add(row);
                var dataTableActions = CreateDataTableActions();

                foreach (var item in instrument.Parts) {
                    var find = dataTableParts.Select("[" + ColPartId + "]=" + item.Id).FirstOrDefault();
                    dataTableActions = AddPartActions(dataTableActions, item);
                    // the next row might have already been added
                    if (find != null) continue;
                    // recursive look to add the rows in the correct order
                    dataTableParts = AddRowsRecursive(item, dataTableParts, 1);
                }
                var tabName = instrument.CreatedDate.ToString("MM-dd-yyyy h:mm:ss tt");
                var worksheetParts = package.Workbook.Worksheets.Add(tabName);
                var worksheetActions = package.Workbook.Worksheets.Add(tabName + " Actions");
                var rangeParts = worksheetParts.Cells.LoadFromDataTable(dataTableParts, true);
                worksheetActions.Cells.LoadFromDataTable(dataTableActions, true);

                // all rows roll up to the instrument
                for (var index = 3; index <= rangeParts.Rows; index++) {
                    worksheetParts.Row(index).OutlineLevel = 1;
                }

                for (var index = 3; index <= rangeParts.Rows; index++) {
                    if (string.IsNullOrWhiteSpace(worksheetParts.Cells[index, 1].Text)) {
                        continue;
                    }
                    var outlineLevel = int.Parse(worksheetParts.Cells[index, 1].Text);
                    if (outlineLevel > 0) {
                        worksheetParts.Row(index).OutlineLevel = outlineLevel+1;
                        worksheetParts.Cells["E" + index + ":L" + index].Style.Indent = outlineLevel;
                    }
                }
                worksheetParts.OutLineSummaryBelow = false;
                // remove all the helper columns
                worksheetParts.DeleteColumn(4);
                worksheetParts.Cells.AutoFitColumns();
            }
            return new MemoryStream(package.GetAsByteArray());
        }

        private static DataTable AddRowsRecursive(
            Part item, 
            DataTable dataTableParts, 
            int level) {
            var newRow = dataTableParts.NewRow();
            newRow[ColLevel] = level; // indent level
            newRow[ColInstrumentName] = "";
            newRow[ColInstrumentSerialNo] = "";
            newRow[ColPartId] = item.Id;
            newRow[ColPartName] = item.Name;
            newRow[ColDescription] = item.Description;
            newRow[ColSapPartNo] = item.SapPartNumber;
            newRow[ColSapPartType] = item.SapPartType;
            newRow[ColDocumentNo] = item.DocumentNumber;
            newRow[ColDashNo] = item.DashNumber;
            newRow[ColRevisionNo] = item.RevisionNumber;
            newRow[ColManufacturer] = item.Manufacturer;
            newRow[ColMfrPartNo] = item.MfgPartNumber;
            newRow[ColLotCode] = item.LotCode;
            newRow[ColDateCode] = item.DateCode;
            dataTableParts.Rows.Add(newRow);
            dataTableParts = AddPartSettings(dataTableParts, item);
            level++;
            foreach (var itemChild in item.Parts) {
                dataTableParts = AddRowsRecursive(itemChild, dataTableParts, level);
            }
            return dataTableParts;
        }
        #endregion
    }
}
