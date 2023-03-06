using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using napelemrendszerek_backend.Models;
using System.Security.Cryptography;
using Comm;

namespace napelemrendszerek_backend
{
    class DbServices
    {
        SolarDBContext SPContext = new SolarDBContext();
        Communication response;
        public DbServices()
        {
            response= new Communication();
            SPContext.Part.Load();
            SPContext.Users.Load();
            SPContext.Roles.Load();
            SPContext.Compartment.Load();
            SPContext.Project.Load();
            SPContext.ProjectStates.Load();
            SPContext.PartProjectConnection.Load();
            SPContext.PartStates.Load();
        }

        public Communication getResponse (){
            return response;
        }

        private void  getAllProjects() {
            //TODO kiszűrni az adott oszlopokat
            response.Message = "nodata";
            response.contentObject = SPContext.Project.ToList();
        }

        private List<Project> getSingleProject(object o) {
            int projectID = Convert.ToInt32(o.ToString());
            return SPContext.Project.Where(p => p.ProjectId == projectID).ToList();
        }
        
        private List<Part> getParts() {
            //TODO kiszűrni a db/dobozt
            return SPContext.Part.ToList();
        }

        private void changeProjectState(int projectID,int stateID ) {//dic

            var selectedProject = SPContext.Project.Where(p => p.ProjectId == projectID).Single();
            selectedProject.ProjectStateId = stateID;

            SPContext.SaveChanges();
        }

        private void addUser(object o) {

            Users singleUser = (Users)o;
            if (!SPContext.Users.Any(i => i.Username == singleUser.Username))
            {
                string hashedPassword = hash(singleUser.UserPassword);
                singleUser.UserPassword = hashedPassword;
                SPContext.Users.Add(singleUser);
                Console.WriteLine($"username: {singleUser.Username} password: {singleUser.UserPassword}");
                SPContext.SaveChanges();
            }
            else {
                Console.WriteLine("hiba, benne van a felhasználó");
                //TODO: jelzés a kliensnek? 
            }
        }
        public void teszt(string asd) {
            Console.WriteLine(hash(asd));
        }
        private void addProject(object o) { 
            Project p = (Project)o;
            //TODO: létezik már?
            SPContext.Project.Add(p);
            SPContext.SaveChanges();
        }

        private void addPart(object o) {
            if (!(o is Part))
            {
                response.Message = "failed";
                //hiba
            }
            Part p = (Part)o;
            //TODO: létezik már?
            SPContext.Part.Add(p);
            SPContext.SaveChanges();
        }
        private void modifyPartPrice(object o)
        {
            //feltételezve hogy egy létező (adatb-ben létező id) partot kapunk, más árral
            Part p = (Part)o;
            var modifiedPart = SPContext.Part.FirstOrDefault(i => i.PartName == p.PartName);
            if (modifiedPart != null)
            {
                modifiedPart.SellPrice = p.SellPrice;
                SPContext.SaveChanges();
            }
            else
            {
                //TODO: hiba jelzése
            }
            
        }
        private string hash(string password) {

            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
        private void login(object o)
        {
            if (!(o is Dictionary<string,string>))
            {
                //hiba
                response.Message = "failed";
                return;
            }
            Dictionary<string, string> userDic = (Dictionary<string, string>)o;
            var user = SPContext.Users.Where(x=>x.Username == userDic["username"]).Single();
            if (user != null)
            {
                if (user.UserPassword == hash(userDic["password"]))
                {
                    response.Message = "successful";
                    response.roleId = user.RoleId;
                }
            }
            else
            {
                //hiba
                response.Message = "failed";
            }
        }
        private void setResponse(string message, object o=null, int? roleId=null) { 
            response.Message = message;
            response.contentObject = o;
            response.roleId = roleId;
        }
        public void requestHandler(Communication comm) {

            switch (comm.Message)
            {
                case "addPart":
                    addPart(comm.contentObject);
                    break;
                case "addProject":
                    addProject(comm.contentObject);
                    break;
                case "addUser":
                    addUser(comm.contentObject);
                    break;
                case "getParts":
                    getParts();
                    break;
                case "getSingleProject":
                    getSingleProject(comm.contentObject);
                    break;
                case "getAllProjects":
                    getAllProjects();
                    break;
                case "modifyPartPrice":
                    modifyPartPrice(comm.contentObject);
                    break;
                case "login":
                    login(comm.contentObject);
                    break;
                // TODO: changeProjectState id-k? 
                default:
                    Console.WriteLine("Nem létező kérés!");
                    break;
            }
        }
    }
}
