﻿
using System.ComponentModel.Design;
using TruckMove.API.DAL.Models;

namespace TruckMove.API.DAL.Repositories.Primary
{
    public interface IContactRepository 
    {
         Task<List<Contact>> GetContactsByCompany(int companyId);

         Task<List<Contact>> GetAllAsync();

    }
}