﻿namespace UMS.Models.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        //All managers will have same email Id to verify the login
        public string DesignationId { get; set; } = string.Empty;

        public IEnumerable<ManagerAttendance> ManagerAttendances { get; set; } 
        public ICollection<RefreshTokenManager> RefreshTokens { get; set; } = [];
    }
}
