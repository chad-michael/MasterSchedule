using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Services;

[WebService(Namespace = "http://www.delta.edu/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoSuggest : WebService
{
    [WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        AddressBookManager adm = new AddressBookManager();
        prefixText = prefixText.ToLower();
        StringCollection list = adm.UserNameList;

        List<string> suggestions = new List<string>();

        int i = 0;

        foreach (string s in list)
        {
            string lowerS = s.ToLower();

            if (lowerS.StartsWith(prefixText))
            {
                suggestions.Add(s);
                if (i == 20) { break; } else { i++; }
            }
        }
        return suggestions.ToArray();
    }
}