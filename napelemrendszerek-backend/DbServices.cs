using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using napelemrendszerek_backend.DBModels;

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
        public void teszt() {
            var a = SPContext.Part.Where(p => p.PartId == 1).Single();
            Console.WriteLine(a.PartId);
        }

        public void addUser() { 
            //TODO
        }

        public void addProject() { 
            //TODO
        }

        public void addPart() { 
            //TODO
        }

    }
}
