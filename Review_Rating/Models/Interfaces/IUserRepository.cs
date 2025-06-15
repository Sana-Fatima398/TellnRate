namespace Review_Rating.Models.Interfaces
{
    public interface IUserRepository
    {
        public List<AppUser> GetAllUsers();

        public string Remove(string id);
    }
}
