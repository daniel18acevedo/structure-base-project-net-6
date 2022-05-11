using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Model.Read;
using Model.Write;

//https://docs.automapper.org/en/stable/
namespace BusinessLogicAdapter.AutoMapper;
public class UserProfile : Profile
{
    public UserProfile()
    {
        this.FromUserModelToUser();
        this.FromUserToUserBasicModel();
        this.FromUserToUserDetailInfoModel();
    }

    private void FromUserModelToUser()
    {
        base.CreateMap<UserModel, User>();
    }

    private void FromUserToUserBasicModel()
    {
        base.CreateMap<User, UserBasicModel>();
    }

    private void FromUserToUserDetailInfoModel()
    {
        base.CreateMap<User, UserDetailInfoModel>();
    }
}