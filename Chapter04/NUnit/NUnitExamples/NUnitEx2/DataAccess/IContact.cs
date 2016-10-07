using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    /// <summary>
    /// Interface to a contact.
    /// Author: Sayed Ibrahim Hashimi (sayed.hashimi@gmail.com)
    /// </summary>
    public interface IContact
    {
        string FirstName
        {
            get;
            set;
        }
        string MiddleName
        {
            get;
            set;
        }
        string LastName
        {
            get;
            set;
        }
        string Email
        {
            get;
            set;
        }
        string Website
        {
            get;
            set;
        }
        /// <summary>
        /// Social security number
        /// </summary>
        string Ssn
        {
            get;
            set;
        }
    }
}
