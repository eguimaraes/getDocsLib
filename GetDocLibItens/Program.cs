using System;
using Microsoft.SharePoint.Client;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Net;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDocLibItens
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Digite a URL do site");
            string URL = Console.ReadLine();
            Console.WriteLine("Digite a URL da Biblioteca");
            string URLlib = Console.ReadLine();
            Console.WriteLine("Digite o usuário");
            string user = Console.ReadLine();
            Console.WriteLine("Digite a Senha");
            string senha = Console.ReadLine();
            ClientContext clientContext = new ClientContext(URL);
            clientContext.Credentials = new SharePointOnlineCredentials(user, GetPassWord(senha));
            Web web = clientContext.Site.RootWeb;
            List list = web.GetList(URLlib);
            CamlQuery query = new CamlQuery() {ViewXml = "<View Scope=\"RecursiveAll\"><Query><Where><IsNotNull><FieldRef Name='FileLeafRef' /></IsNotNull></Where></Query><ViewFields><FieldRef Name='ContentType' /><FieldRef Name='DocIcon' /><FieldRef Name='FileLeafRef' /><FieldRef Name='ComplianceAssetId' /><FieldRef Name='Title' /><FieldRef Name='PublishingStartDate' /><FieldRef Name='PublishingExpirationDate' /><FieldRef Name='DocumentSetDescription' /><FieldRef Name='ID' /><FieldRef Name='Created' /><FieldRef Name='Author' /><FieldRef Name='Modified' /><FieldRef Name='Editor' /><FieldRef Name='_CopySource' /><FieldRef Name='CheckoutUser' /><FieldRef Name='_CheckinComment' /><FieldRef Name='LinkFilenameNoMenu' /><FieldRef Name='LinkFilename' /><FieldRef Name='FileSizeDisplay' /><FieldRef Name='ItemChildCount' /><FieldRef Name='FolderChildCount' /><FieldRef Name='_ComplianceFlags' /><FieldRef Name='_ComplianceTag' /><FieldRef Name='_ComplianceTagWrittenTime' /><FieldRef Name='_ComplianceTagUserId' /><FieldRef Name='_IsRecord' /><FieldRef Name='_CommentCount' /><FieldRef Name='_LikeCount' /><FieldRef Name='_DisplayName' /><FieldRef Name='AppAuthor' /><FieldRef Name='AppEditor' /><FieldRef Name='Edit' /><FieldRef Name='_UIVersionString' /><FieldRef Name='ParentVersionString' /><FieldRef Name='ParentLeafName' /></ViewFields><QueryOptions /></View>" }; 
            ListItemCollection itens = list.GetItems(query);
            clientContext.Load(web);
            clientContext.Load(list);
            clientContext.Load(itens);
            clientContext.ExecuteQuery();
            foreach (ListItem item in itens) { 
            Console.WriteLine("Pasta do Arquivo:{1} - Nome do Arquivo: {0} ",item["FileLeafRef"],item["FileRef"].ToString().Replace(item["FileLeafRef"].ToString(),""));
            }
            Console.ReadLine();





        }

        public static SecureString GetPassWord(string password)
        {
            SecureString securePassword = new SecureString();
            foreach (char c in password) { securePassword.AppendChar(c); }
            return securePassword;
        }
    }
}
