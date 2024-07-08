using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Product Added Succesfully";
        public static string ProductNameInvalid = "Product Name Is Invalid";
        public static string MaintenanceTime = "In Maintenance";
        public static string ProductListed = "Product Listed Succesfully";
        public static string AuthorizationDenied="Access Denied ";
        public static string UserRegistered="User is registered succesfuly";
        public static string UserNotFound="User is not found";
        public static string PasswordError = "Password incorrect";
        public static string SuccessfulLogin="Login Succesfull";
        public static string UserAlreadyExists="This user is already exists";
        public static string AccessTokenCreated="Token is created";
        public static string ProductDeleted = "Product Deleted Succesfully";
        public static string CategoryNotFound = "This category does not exists";
    }
}
