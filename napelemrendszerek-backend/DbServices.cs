using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using napelemrendszerek_backend.DBModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

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

        public List<Project> getSingleProject(int projectID) {

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

        public void addUser(Users singleUser) {
            //TODO ell. username
            string hashedPassword = hash(singleUser.UserPassword);
            singleUser.UserPassword = hashedPassword;
            SPContext.Users.Add(singleUser);
            Console.WriteLine($"username: {singleUser.Username} password: {singleUser.UserPassword}");
            SPContext.SaveChanges();
        }

        public void addProject() { 
            //TODO
        }

        public void addPart() { 
            //TODO
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
    }
}
