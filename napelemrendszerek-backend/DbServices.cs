using Microsoft.EntityFrameworkCore;
using SolarPanelSystem_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using napelemrendszerek_backend.Models;

namespace napelemrendszerek_backend
{
    class DbServices
    {
        SolarPanelContext SPContext = new SolarPanelContext();
        public DbServices()
        {
            SPContext.Parts.Load();
            SPContext.Users.Load();
            SPContext.Roles.Load();
            SPContext.Compartments.Load();
            SPContext.Projects.Load();
            SPContext.ProjectStates.Load();
            SPContext.PartProjectConnections.Load();
            SPContext.PartStates.Load();
        }

        public List<Project>  getAllProjects() {
            //TODO kiszűrni az adott oszlopokat
            return SPContext.Projects.ToList();
        }

        public List<Project> getSingleProject(int projectID) {

            return SPContext.Projects.Where(p => p.projectID == projectID).ToList();
        }

        public List<Part> getParts() {
            //TODO kiszűrni a db/dobozt
            return SPContext.Parts.ToList();
        }

        public void changeProjectState(int projectID,int stateID ) {

            var selectedProject = SPContext.Projects.Where(p => p.projectID == projectID).Single();
            selectedProject.projectStateID = stateID;

            SPContext.SaveChanges();
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
