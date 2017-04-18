using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AddressBookManager
/// </summary>
public class AddressBookManager
{
    public StringCollection UserNameList
    {
        get { return (StringCollection)HttpContext.Current.Application["AddressList"]; }
    }

    public AddressBookManager()
    {
    }

    public void LoadFromDb()
    {
        if (HttpContext.Current.Application["AddressList"] == null)
        {
            ERPDataContext db = new ERPDataContext();
            var x = db.Bios.Where(b => b.Type != "05");
            StringCollection addressList = new StringCollection();
            foreach (var y in x)
            {
                addressList.Add(y.LastNM + ", " + y.FirstNM + " - " + y.IDNO);
            }

            HttpContext.Current.Application.Add("AddressList", addressList);
        }
    }
}