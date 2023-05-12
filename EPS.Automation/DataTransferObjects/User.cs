using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Automation.DataTransferObjects
{
    public class User : BaseEntityObject
    {
        /// <summary>
        /// This is the password.
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// This is the Mobile number.
        /// </summary>
        public String MobileNo { get; set; }

        /// <summary>
        /// This is the Designation.
        /// </summary>
        public String Designation { get; set; }
        /// <summary>
        /// This is the Group.
        /// </summary>
        public String Group { get; set; }
        /// <summary>
        /// This is the Role.
        /// </summary>
        public String Role { get; set; }


        /// <summary>
        /// This is the type of the user
        /// </summary>
        public enum UserTypeEnum
        {
            #region User Types

            EPSAdminUser01=0,
            NewEPSUser01=1,
            EPSSuperAdminUser01=2,

            #endregion User Types
        }

        /// <summary>
        /// This is the type of the user.
        /// </summary>
        public UserTypeEnum UserType { get; set; }

        /// <summary>
        /// This is the user Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// This method selects users based on given condition.
        /// </summary>
        /// <param name="predicate">The condition</param>
        /// <returns>A list of users</returns>
        public static List<User> Get(Func<User, bool> predicate)
        {
            return InMemoryDatabaseSingleton.DatabaseInstance.SelectMany(predicate);
        }

        /// <summary>
        /// This method inserts a new user into the system.
        /// </summary>]
        public void StoreUserInMemory()
        {
            InMemoryDatabaseSingleton.DatabaseInstance.Insert(this);
        }

        /// <summary>
        /// This method is used to update the user.
        /// </summary>
        public void UpdateUserInMemory(User user)
        {
            InMemoryDatabaseSingleton.DatabaseInstance.Update(user);
        }

        /// <summary>
        /// This method is used to write the user.
        /// </summary>
        public void WriteUserInMemory(User user, string entityType, string entityValue)
        {
            InMemoryDatabaseSingleton.SaveUserTestData(user, entityType, entityValue);
        }

        /// <summary>
        /// This method selects a single user based on the role.
        /// </summary>
        /// <param name="userType">This is the user type.</param>
        /// <returns>A single user.</returns>
        public static User Get(UserTypeEnum userType)
        {
            return InMemoryDatabaseSingleton.DatabaseInstance.SelectMany
                <User>(x => x.UserType == userType && x.IsCreated).OrderByDescending(x => x.CreationDate).First();
        }

        /// <summary>
        /// This method gets the user based on User ID.
        /// </summary>
        /// <param name="userId">This is user ID.</param>
        /// <returns>User based on ID.</returns>
        public static User Get(string userId)
        {
            return InMemoryDatabaseSingleton.DatabaseInstance.SelectMany<User>(x => x.UserId == userId && x.IsCreated)
                .OrderByDescending(x => x.CreationDate).First();
        }

        /// <summary>
        /// This method is used to update the user's password.
        /// </summary>
        /// <param name="password">This is the password of the user.</param>
        public void UpdatePassword(string password)
        {
            User user = InMemoryDatabaseSingleton.DatabaseInstance.SelectTopOne<User>(x => x.Name == Name);
            user.Password = password;
        }

        /// <summary>
        /// This method returns all created users of the given type.
        /// </summary>
        /// <param name="userType">This is the type of the user.</param>
        /// <returns>User List.</returns>
        public static List<User> GetAll(UserTypeEnum userType)
        {
            return InMemoryDatabaseSingleton.DatabaseInstance.SelectMany<User>(
                x => x.UserType == userType).OrderByDescending(
                x => x.CreationDate).ToList();
        }
        /// <summary>
        /// This is Email type
        /// </summary>
        public string Email { get; set; }



        /// <summary>
        /// This method is used to update the user's Email ID.
        /// </summary>
        /// <param name="email">This is the email id of the user.</param>
        public void UpdateEmail(string email)
        {
            User user = InMemoryDatabaseSingleton.DatabaseInstance.SelectTopOne<User>(x => x.Name == Name);
            user.Email = email;
        }

        /// <summary>
        /// This method is used to update the user's Mobile Number.
        /// </summary>
        /// <param name="mobileNo">This is the mobile number of the user.</param>
        public void UpdateMobileNo(string mobileNo)
        {
            User user = InMemoryDatabaseSingleton.DatabaseInstance.SelectTopOne<User>(x => x.Name == Name);
            user.MobileNo = mobileNo;
        }

        /// <summary>
        /// This method is used to update the user's Full Name.
        /// </summary>
        /// <param name="name">This is the email id of the user.</param>
        public void UpdateName(string name)
        {
            User user = InMemoryDatabaseSingleton.DatabaseInstance.SelectTopOne<User>(x => x.Name == Name);
            user.Name = name;
        }

        /// <summary>
        /// This method is used to update the user's designation.
        /// </summary>
        /// <param name="designation">This is the designation of the user.</param>
        public void UpdateDesignation(string designation)
        {
            User user = InMemoryDatabaseSingleton.DatabaseInstance.SelectTopOne<User>(x => x.Name == Name);
            user.Designation = designation;
        }
        /// <summary>
        /// This method is used to update the user's group.
        /// </summary>
        /// <param name="groupName">This is the groupName of the user.</param>
        public void UpdateGroup(string groupName)
        {
            User user = InMemoryDatabaseSingleton.DatabaseInstance.SelectTopOne<User>(x => x.Name == Name);
            user.Group = groupName;
        }
        /// <summary>
        /// This method is used to update the user's role.
        /// </summary>
        /// <param name="role">This is the role of the user.</param>
        public void UpdateRole(string role)
        {
            User user = InMemoryDatabaseSingleton.DatabaseInstance.SelectTopOne<User>(x => x.Name == Name);
            user.Role = role;
        }
    }
}