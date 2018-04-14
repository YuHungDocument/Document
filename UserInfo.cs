using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class UserInfo
    {
        private string _EID;

        public string EID
        {
            get { return _EID; }
            set { _EID = value; }
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Department;

        public string Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        private string _position;

        public string position
        {
            get { return _position; }
            set { _position = value; }
        }

        private string _Gender;

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        private string _UserID;

        public string UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private string _Pwd;

        public string Pwd
        {
            get { return _Pwd; }
            set { _Pwd = value; }
        }

        private string _Tel;

        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }

        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        private string _Birthday;
        public string Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        private string _Cel;
        public string Cel
        {
            get { return _Cel; }
            set { _Cel = value; }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _agent;
        public string agent
        {
            get { return _agent; }
            set { _agent = value; }
        }

        private string _PK;
        public string PK
        {
            get { return _PK; }
            set { _PK = value; }
        }

        private string _KeyAddress;
        public string KeyAddress
        {
            get { return _KeyAddress; }
            set { _KeyAddress = value; }
        }

        private int _Permission;
        public int Permission
        {
            get { return _Permission; }
            set { _Permission = value; }
        }

        private int _temp_Permission;
        public int temp_Permission
        {
            get { return _temp_Permission; }
            set { _temp_Permission = value; }
        }
    }
}