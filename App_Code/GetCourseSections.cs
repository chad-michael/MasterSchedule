using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Summary description for GetCourseSections
/// </summary>
public static class GetCourseSections
{
    public static string GetCreditType(string sTerm, string sSectionNumber, string sCourseNumber)
    {
        using (ODSDataContext db = new ODSDataContext())
        {
            List<D04_ODS_COURSE_SECTION> theseSections =
            db.D04_ODS_COURSE_SECTIONs.Where(x => x.POS == 1
                                                && x.SEC_NAME == (sCourseNumber + "=" + sSectionNumber)
                                                && x.SEC_TERM == sTerm).ToList();

            if (theseSections.Count() > 0)
            {
                return theseSections.First().SEC_CRED_TYPE;
            }
            else
            {
                return "2";
            }
        }
    }
}