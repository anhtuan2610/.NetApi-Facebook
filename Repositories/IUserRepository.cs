using Facebook_be.Models;

namespace Facebook_be.Repositories
{
    public interface IUserRepository
    {
        User Create(User user); // sử dụng để thêm một bản ghi mới vào cơ sở dữ liệu. Trong trường hợp này, Create sẽ thêm một đối tượng User vào bảng users và trả về đối tượng User đã được thêm, thường kèm theo ID của đối tượng đó.
        User GetByUsername(string username);
        User GetById(int id);
    }
}
