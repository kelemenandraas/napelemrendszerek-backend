using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using napelemrendszerek_backend.DBModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Comm;

namespace napelemrendszerek_backend
{
    class DbServices
    {
        SolarDBContext SPContext = new SolarDBContext();
        public DbServices()
        {
            SPContext.Part.Load();
            SPContext.Users.Load();
            SPContext.Roles.Load();
            SPContext.Compartment.Load();
            SPContext.Project.Load();
            SPContext.ProjectStates.Load();
            SPContext.PartProjectConnection.Load();
            SPContext.PartStates.Load();
        }

        public List<Project>  getAllProjects() {
            //TODO kiszűrni az adott oszlopokat
            return SPContext.Project.ToList();
        }

        public List<Project> getSingleProject(object o) {
            int projectID = Convert.ToInt32(o.ToString());
            return SPContext.Project.Where(p => p.ProjectId == projectID).ToList();
        }

        public List<Part> getParts() {
            //TODO kiszűrni a db/dobozt
            return SPContext.Part.ToList();
        }

        public void changeProjectState(int projectID,int stateID ) {

            var selectedProject = SPContext.Project.Where(p => p.ProjectId == projectID).Single();
            selectedProject.ProjectStateId = stateID;

            SPContext.SaveChanges();
        }

        public void teszt(object r) {
            Users u = (Users)r;
            Console.WriteLine("Teszt");
            Console.WriteLine(u.ToString());
        }

        public void addUser(object o) {
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

        public void addProject(object o) { 
            Project p = (Project)o;
            //TODO: létezik már?
            SPContext.Project.Add(p);
            SPContext.SaveChanges();
        }

        public void addPart(object o) { 
            Part p = (Part)o;
            //TODO: létezik már?
            SPContext.Part.Add(p);
            SPContext.SaveChanges();
        }
        public void modifyPartPrice(object o)
        {
            //feltételezve hogy egy létező (adatb-ben létező id) partot kapunk, más árral
            Part p = (Part)o;
            var modifiedPart = SPContext.Part.FirstOrDefault(i => i.PartId == p.PartId);
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

            byte[] salt = new byte[16]; 
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

        public void requestHandler(Communication comm) {

            switch (comm.requestName)
            {
                case "addPart":
                    addPart(comm.parameterObject);
                    break;
                case "addProject":
                    addProject(comm.parameterObject);
                    break;
                case "addUser":
                    addUser(comm.parameterObject);
                    break;
                case "getParts":
                    getParts();
                    break;
                case "getSingleProject":
                    getSingleProject(comm.parameterObject);
                    break;
                case "getAllProjects":
                    getAllProjects();
                    break;
                // TODO: changeProjectState id-k? 
                default:
                    Console.WriteLine("Nem létező kérés!");
                    break;
            }
        }
    }
}
