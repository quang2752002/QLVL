using GUIs.Helper;
using GUIs.Models.EF;
using GUIs.Models.VIEW;

namespace GUIs.Models.DAO
{
    public class UserDAO
    {
        private QLVLContext context = new QLVLContext();
        public void InsertOrUpdate(User item)
        {
            if (item.Id == 0)
            {
                context.Users.Add(item);
            }
            context.SaveChanges();
        }
        public User getItem(int id)
        {
            var query = context.Users.Where(x => x.Id == id).FirstOrDefault();
            
            return query;
        }
        public UserVIEW getItemView(int id)
        {
            var query = (from a in context.Users
                         where a.Id == id
                         select new UserVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Email = a.Email,
                             Username = a.Username,
                             Password = a.Password,
                             State = a.State
                         }).FirstOrDefault();

            return query;
        }
        public List<UserVIEW> Search( out int total, String name = "", string state="admin", int index = 1, int size = 10)
        {
            if (name == null) name = "";

            var query = (from a in context.Users
                         where (a.Name.Contains(name)&&a.State==state)
                         select new UserVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Email = a.Email,
                             Username=a.Username,
                             Password = a.Password,
                             State=a.State
                         }).ToList();
            if (!string.IsNullOrEmpty(name) && name != "")
            {
                query = query.Where(x => x.Name.Contains(name)).ToList();
            }
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }
        
      
        public List<UserVIEW> getUser(out int total, int id, int index = 1, int size = 10)
        {

            var query = (from a in context.Users                  
                         where a.Id == id 
                         select new UserVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Email = a.Email,
                             Username = a.Username,
                             Password = a.Password,
                             State = a.State
                         }).ToList();
            total = query.Count();

            if (size > 0 && index > 0)
            {
                query = query.Skip((index - 1) * size).Take(size).ToList();
            }
            return query;
        }

        public void Detele(int id)
        {
            User x = getItem(id);
            context.Users.Remove(x);
            context.SaveChanges();
        }
        public int Login(string username, string password)
        {
            var query = (from a in context.Users
                         where a.Username == username && a.Password == password
                         select new UserVIEW
                         {
                             Id = a.Id,
                         }).FirstOrDefault();
            if (query != null)
                return query.Id;
            return -1;
        }
        public Boolean CheckDangKy(string username)
        {
            var query = (from a in context.Users
                         where (a.Email == username ||  a.Username == username)
                         select new UserVIEW
                         {
                             Id = a.Id,
                             Name = a.Name,

                           

                         }).FirstOrDefault();
            if (query != null)
                return false;
            return true;
        }
    }
}
