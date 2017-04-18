using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for ReportInformation
/// </summary>
public static class FacultyInformation
{
    public static List<FacultyIdno> GetFacultyIdno(string firstName, string lastName, string userName)
    {
        using (ERPDataContext reportData = new ERPDataContext())
        {
            if ((firstName.Length + lastName.Length + userName.Length) > 0)
            {
                string convertedUserName = userName.Replace("@delta.edu", "");

                List<FacultyIdno> reportInfo = (from s in reportData.Bios
                                                join u in reportData.UNames on s.IDNO equals u.IDNO
                                                where s.FirstNM.Contains(firstName) &&
                                                        s.LastNM.Contains(lastName) &&
                                                        u.Usernames.Contains(convertedUserName) &&
                                                        s.Type != "05" &&
                                                        s.Type != "5"

                                                select new FacultyIdno()
                                                {
                                                    DeltaId = s.IDNO.ToString(),
                                                    FirstName = s.FirstNM,
                                                    LastName = s.LastNM,
                                                    Gender = s.Gender.ToUpper()
                                                }).OrderBy(x => x.LastName).ToList();
                return reportInfo;
            }
            else
            {
                return new List<FacultyIdno>();
            }
        }
    }
}

public class FacultyIdno
{
    public string DeltaId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Gender { get; set; }
}