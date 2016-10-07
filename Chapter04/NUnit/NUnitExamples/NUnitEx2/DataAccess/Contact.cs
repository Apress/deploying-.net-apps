using System;

using System.Collections.Generic;
using System.Text;

using System.Data;
namespace DataAccess
{
    public class Contact : IContact
    {
        #region Private members

        private string _firstName;
        private string _lastName;
        private string _middleName;
        private string _email;
        private string _url;
        private string _ssn;
        #endregion

        #region IContact Members

        public string FirstName
        {
            get
            {
                return this._firstName;
            }
            set
            {
                this._firstName = value;
            }
        }

        public string MiddleName
        {
            get
            {
                return this._middleName;
            }
            set
            {
                this._middleName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this._lastName;
            }
            set
            {
                this._lastName = value;
            }
        }
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                this._email = value;
            }
        }
        public string Website
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        public string Ssn
        {
            get
            {
                return this._ssn;
            }
            set
            {
                this._ssn = value;
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Contact [");
            sb.Append(this.FirstName);
            sb.Append(",");
            sb.Append(this.MiddleName);
            sb.Append(",");
            sb.Append(this.LastName);
            sb.Append(",");
            sb.Append(this.Ssn);
            sb.Append(",");
            sb.Append(this.Website);
            sb.Append(",");
            sb.Append(this.Email);
            sb.Append("]");

            return sb.ToString();
        }

        public static IList<IContact> BuildContacts(DataSet ds)
        {
            IList<IContact> contacts = new List<IContact>();

            if (ds == null || ds.Tables.Count == 0)
            {
                return contacts;
            }

            

            DataTable dt = ds.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                IContact current = new Contact();
                current.FirstName = row["FirstName"].ToString();
                current.MiddleName = row["MiddleName"].ToString();
                current.LastName = row["LastName"].ToString();
                current.Ssn = row["Ssn"].ToString();
                current.Website = row["Website"].ToString();
                current.Email = row["Email"].ToString();
                contacts.Add(current);
            }

            return contacts;
        }

    }
}
