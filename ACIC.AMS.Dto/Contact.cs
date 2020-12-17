namespace ACIC.AMS.Dto
{
    public class Contact
    {
        private string middleName;
        private string mblDirect;
        private string mblMobile;
        private string mblBusiness;
        private string email;
        private string email2;
        private string notes;
        private string title;

        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get { return middleName == null ? string.Empty : middleName; } set { middleName = value; } }
        public string LastName { get; set; }
        public float? TitleId { get; set; }
        public string Title { get { return title == null ? string.Empty : title; } set { title = value; } }
        public string MblBusiness { get { return mblBusiness == null ? string.Empty : mblBusiness; } set { mblBusiness = value; } }
        public string MblDirect { get { return mblDirect == null ? string.Empty : mblDirect; } set { mblDirect = value; } }
        public string MblMobile { get { return mblMobile == null ? string.Empty : mblMobile; } set { mblMobile = value; } }
        public string Email1 { get { return email == null ? string.Empty : email; } set { email = value; } }
        public string Email2 { get { return email2 == null ? string.Empty : email2; } set { email2 = value; } }
        public string Type { get; set; }
        public string RefId { get; set; }
        public string Notes { get { return notes == null ? string.Empty : notes; } set { notes = value; } }
        public string FullName { get { return $"{FirstName} {MiddleName} {LastName}"; } }
    }
}
