using System;
using System.Linq;
using AutoMapper;
using Bd.Icm.Core;
using Bd.Icm.DataAccess.Database;

namespace Bd.Icm.Web
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserInfo, Dto.User>();
                cfg.CreateMap<User, Dto.User>()
                    .ForMember(m => m.Roles, opt => opt.MapFrom(s => s.Roles.Select(x => x.Role)));
                cfg.CreateMap<Dto.User, User>()
                    .ForMember(m => m.Roles, opt => opt.Ignore())
                    .ForMember(m => m.IsNew, opt => opt.Ignore());

                cfg.CreateMap<spSearchInstrumentParts_Result, DataAccess.Dto.PartSearchResult>(MemberList.Destination);
                cfg.CreateMap<PartSearchResult, Dto.PartSearchResult>(MemberList.Destination);

                cfg.CreateMap<InstrumentInfo, Dto.Instrument>(MemberList.Destination)
                    .ForMember(m => m.SapPartType, o => o.ResolveUsing<InstrumentInfoTypeResolver>());
                cfg.CreateMap<Instrument, Dto.Instrument>(MemberList.Destination)
                    .ForMember(m => m.SapPartType, o => o.ResolveUsing<InstrumentTypeResolver>());
                cfg.CreateMap<Dto.Instrument, Instrument>(MemberList.Destination)
                    .ForMember(m => m.Parts, o => o.Ignore())
                    .ForMember(m => m.SapPartType, o => o.MapFrom(f => (InstrumentType)f.SapPartType.InstrumentTypeId));

                cfg.CreateMap<PartInfo, Dto.Part>(MemberList.Destination)
                    .ForMember(m => m.SapPartType, o => o.ResolveUsing<PartTypeResolver>());

                cfg.CreateMap<Part, Dto.Part>(MemberList.Destination)
                    .ForMember(m => m.SapPartType, o => o.ResolveUsing<PartTypeResolver>());
                cfg.CreateMap<Dto.Part, Part>(MemberList.Destination)
                    .ForMember(m => m.Parts, o => o.Ignore())
                    .ForMember(m => m.Actions, o => o.Ignore())
                    .ForMember(m => m.Metadata, o => o.Ignore())
                    .ForMember(m => m.SapPartType, o => o.MapFrom(f => (PartType)f.SapPartType.PartTypeId));

                cfg.CreateMap<Dto.InstrumentCommit, InstrumentCommit>();
                cfg.CreateMap<InstrumentCommitInfo, Dto.InstrumentCommit>();
                cfg.CreateMap<InstrumentCommit, Dto.InstrumentCommit>();

                cfg.CreateMap<PartNode, Dto.PartNode>(MemberList.Destination);

                cfg.CreateMap<PartAction, Dto.PartAction>(MemberList.Destination)
                    .ForMember(m => m.Action, o => o.ResolveUsing<PartActionTypeResolver>());
                cfg.CreateMap<Dto.PartAction, PartAction>(MemberList.Destination)
                    .ForMember(m => m.Modifier, o => o.Ignore())
                    .ForMember(m => m.Action, o => o.MapFrom(f => (PartActionType)f.Action.PartActionTypeId));

                cfg.CreateMap<PartMetadata, Dto.PartMetadata>(MemberList.Destination);
                cfg.CreateMap<Dto.PartMetadata, PartMetadata>(MemberList.Destination)
                    .ForMember(m => m.Modifier, o => o.Ignore());


                cfg.CreateMap<PartChange, Dto.PartChange>(MemberList.Destination);
                cfg.CreateMap<PartMetadataChange, Dto.PartMetadataChange>(MemberList.Destination);

                cfg.CreateMap<ChangeUser, Dto.ChangeUser>(MemberList.Destination);

                cfg.CreateMap<InstrumentDiff, Dto.InstrumentDiff>();
                cfg.CreateMap<PartVersion, Dto.PartVersion>();

                cfg.CreateMap<PartChange, Dto.UncommittedChange>()
                    .ForMember(m => m.Id, opt => opt.MapFrom(f => f.Id))
                    .ForMember(m => m.EffectiveTo, opt => opt.MapFrom(f => f.EffectiveTo))
                    .ForMember(m => m.RecordType, opt => opt.UseValue(RecordType.Part))
                    .ForMember(m => m.Location, opt => opt.ResolveUsing<PartChangeLocationResolver>());
                cfg.CreateMap<PartMetadataChange, Dto.UncommittedChange>()
                    .ForMember(m => m.Id, opt => opt.MapFrom(f => f.Id))
                    .ForMember(m => m.EffectiveTo, opt => opt.MapFrom(f => f.EffectiveTo))
                    .ForMember(m => m.RecordType, opt => opt.UseValue(RecordType.PartMetadata))
                    .ForMember(m => m.Location, opt => opt.ResolveUsing<PartMetadataChangeLocationResolver>());
            });
        }

    }

    public class InstrumentTypeResolver : ValueResolver<Instrument, Dto.InstrumentType>
    {
        protected override Dto.InstrumentType ResolveCore(Instrument source)
        {
            switch (source.SapPartType)
            {
                case InstrumentType.None:
                    return new Dto.InstrumentType { InstrumentTypeId = 0, Name = "N/A" };
                case InstrumentType.Fert:
                    return new Dto.InstrumentType { InstrumentTypeId = 1, Name = "FERT" };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public class InstrumentInfoTypeResolver : ValueResolver<InstrumentInfo, Dto.InstrumentType>
    {
        protected override Dto.InstrumentType ResolveCore(InstrumentInfo source)
        {
            switch (source.SapPartType)
            {
                case InstrumentType.None:
                    return new Dto.InstrumentType { InstrumentTypeId = 0, Name = "N/A" };
                case InstrumentType.Fert:
                    return new Dto.InstrumentType { InstrumentTypeId = 1, Name = "FERT" };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public class PartTypeResolver : ValueResolver<IPart, Dto.PartType>
    {
        protected override Dto.PartType ResolveCore(IPart source)
        {
            switch (source.SapPartType)
            {
                case PartType.None:
                    return new Dto.PartType { PartTypeId = 0, Name = "N/A" };
                case PartType.Halb:
                    return new Dto.PartType { PartTypeId = 1, Name = "HALB" };
                case PartType.Roh:
                    return new Dto.PartType { PartTypeId = 2, Name = "ROH" };
                case PartType.Zicp:
                    return new Dto.PartType { PartTypeId = 3, Name = "ZICP" };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public class PartActionTypeResolver : ValueResolver<PartAction, Dto.PartActionType>
    {
        protected override Dto.PartActionType ResolveCore(PartAction source)
        {
            switch (source.Action)
            {
                case PartActionType.None:
                    return new Dto.PartActionType { PartActionTypeId = 0, Name = "None" };
                case PartActionType.Calibration:
                    return new Dto.PartActionType { PartActionTypeId = 1, Name = "Calibration" };
                case PartActionType.Repair:
                    return new Dto.PartActionType { PartActionTypeId = 2, Name = "Repair" };
                case PartActionType.Settings:
                    return new Dto.PartActionType { PartActionTypeId = 3, Name = "Settings" };
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public class PartChangeLocationResolver : ValueResolver<PartChange, string>
    {
        protected override string ResolveCore(PartChange source)
        {
            return $"{source.Name}, {source.Description}, {source.DocumentNumber}, {source.DashNumber}";
        }
    }

    public class PartMetadataChangeLocationResolver : ValueResolver<PartMetadataChange, string>
    {
        protected override string ResolveCore(PartMetadataChange source)
        {
            return $"{source.Name}, {source.Description} | Setting: {source.MetaKey} = {source.MetaValue}";
        }
    }

}