using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckMove.API.BLL.Models.Primary;
using TruckMove.API.BLL.Models.PrimaryDTO;
using TruckMove.API.BLL.Models.UserManagmentDTO;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.BLL.Models.ModelConvertors
{
    public class UserConvertor
    {
        public static UserOutputDto ConvertUser(UserModel userModel)
        {
           
            UserOutputDto user = new UserOutputDto();
            user.Id = userModel.Id;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Email = userModel.Email;
            user.PhoneNumber = userModel.PhoneNumber;
            return user;

        }
        public static UserModel ConvertUser(UserOutputDto userDto)
        {

            UserModel user = new UserModel();
            user.Id = userDto.Id;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            return user;

        }
        public static List<UserOutputDto> ConvertToUserList(List<UserModel> userModels)
        {
            List<UserOutputDto> users = new List<UserOutputDto>();
            foreach (var usermodel in userModels)
            {
                users.Add(ConvertUser(usermodel));
            }
            return users;
        }

    }
}
