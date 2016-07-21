namespace Bd.Icm
{
    public interface ICommittable
    {
        bool IsCommittable { get; }
        bool IsSelfCommittable { get; }
        bool IsCommittableForUser(int userId);
        bool IsSelfCommittableForUser(int userId);
        void Commit(int userId, int instrumentCommitId);
    }
}
