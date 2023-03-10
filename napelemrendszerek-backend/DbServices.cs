﻿using Microsoft.EntityFrameworkCore;
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
            //role ellenőrzés?
            List<Project> projects = SPContext.Project.ToList();
            if (projects.Count == 0)
            {
                response.Message = "nodata";
            }
            else
            {
                response.Message = "successful";
                response.contentObject = projects;
            }
        }

        private void getSingleProject(object o) {
            //role ellenőrzés?
            int projectID = Convert.ToInt32(o.ToString());
            Project project = SPContext.Project.FirstOrDefault(p => p.ProjectId == projectID);
            if (project != null)
            {
                response.Message = "successful";
                response.contentObject = project;
            }
            else
            {
                response.Message = "failed";
            }
        }
        
        private void getParts() {
            
            //role ellenőrzés?
            List<Part> parts = SPContext.Part.ToList();
            if (parts.Count == 0)
            {
                response.Message = "nodata";
            }
            else
            {
                response.Message = "successful";
                response.contentObject = parts;
            }
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
            if (!(o is Dictionary<string, string>))
            {
                setResponse("failed");
                return;
            }
            Dictionary<string, string> modifiedPartDic = (Dictionary<string, string>)o;
            //helyes kulcsok ellenőrzése
            if (!(modifiedPartDic.ContainsKey("partName")) || !(modifiedPartDic.ContainsKey("sellPrice")))
            {
                setResponse("failed");
                return;
            }
            var modifiedPart = SPContext.Part.FirstOrDefault(i => i.PartName == modifiedPartDic["partName"]);
            if (modifiedPart != null)
            {
                if (int.TryParse(modifiedPartDic["sellPrice"], out int newPartPrice))
                {
                    modifiedPart.SellPrice = newPartPrice;
                    SPContext.SaveChanges();
                    setResponse("successful");
                }
                else
                {
                    setResponse("failed");
                    return;
                }

            }
            else
            {
                //nincs olyan alkatrész
                setResponse("nodata");
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
                setResponse("failed");
                return;
            }
            Dictionary<string, string> userDic = (Dictionary<string, string>)o;
            var user = SPContext.Users.Select(x=>x).Where(x=>x.Username == userDic["username"]).FirstOrDefault();
            if (user != null)
            {
                if (user.UserPassword == hash(userDic["password"]))
                {
                    setResponse("successful", null, user.RoleId);
                }
                else 
                {
                    setResponse("wrongpassword");
                }
            }
            else
            {
                //hiba
                setResponse("nodata");
            }
        }
        private void setResponse(string message, object o=null, int? roleId=null) { 
            response.Message = message;
            response.contentObject = o;
            response.roleId = roleId;
        }
        public void requestHandler(Communication comm)
        {
            // roleID 1 - Raktarvezeto, 2 - Raktaros, 3 - Szakember
            int? roleID = comm.roleId;

            switch (comm.Message)
            {
                case "addPart":
                    if (roleID == 1)
                    {
                        addPart(comm.contentObject);
                    }
                    else
                    {
                        setResponse("denied");
                    }
                    break;

                case "addProject":
                    if (roleID == 3)
                    {
                        addProject(comm.contentObject);
                    }
                    else
                    {
                        setResponse("denied");
                    }
                    break;

                case "addUser":
                    addUser(comm.contentObject);
                    break;

                case "getParts":
                    if (roleID == 1 || roleID == 2)
                    {
                        getParts();
                    }
                    else
                    {
                        setResponse("denied");
                    }
                    break;

                case "getSingleProject":
                    if (roleID == 2 || roleID == 3)
                    {
                        getSingleProject(comm.contentObject);
                    }
                    else
                    {
                        setResponse("denied");
                    }
                    break;

                case "getAllProjects":
                    if (roleID == 2 || roleID == 3)
                    {
                        getAllProjects();
                    }
                    else
                    {
                        setResponse("denied");
                    }
                    break;

                case "modifyPartPrice":
                    if (roleID == 1)
                    {
                        modifyPartPrice(comm.contentObject);
                    }
                    else
                    {
                        setResponse("denied");
                    }
                    break;

                case "login":
                    login(comm.contentObject);
                    break;

                // TODO: changeProjectState id-k? 

                default:
                    setResponse("unknown request");
                    break;
            }
        }
    }
}
