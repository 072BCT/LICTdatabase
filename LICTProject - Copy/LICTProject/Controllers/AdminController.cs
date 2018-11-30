//using LICTProject.Models;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace LICTProject.Controllers
//{
//    public class AdminController : Controller
//    {
//        // GET: Admin
//        public ActionResult Index()
//        {
//            return View();
//        }

//        public JsonResult GetEmpChart()
//        {
//            List<Personnel> PerChartList = new List<Personnel>();

//            string query = "SELECT * FROM dbo.Personnel";


//            // Get it from Web.config file
//            string connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
//            using (SqlConnection con = new SqlConnection(connetionString))
//            {
//                using (SqlCommand cmd = new SqlCommand(query))
//                {
//                    cmd.CommandType = CommandType.Text;
//                    cmd.Connection = con;
//                    con.Open();
//                    using (SqlDataReader dr = cmd.ExecuteReader())
//                    {
//                        while (dr.Read())
//                        {
//                            // Adding new Employee object to List
//                            PerChartList.Add(new Personnel()
//                            {
//                                PersonnelID = dr.GetInt32(0),
//                                FullName = dr.GetString(1),
//                                LICT_Title = dr.GetInt32(5),
//                                Email = dr.GetString(7),
                              
//                            });
//                        }
//                    }
//                    con.Close();
//                }
//            }

//            return Json(PerChartList, JsonRequestBehavior.AllowGet);
//        }
//    }
//}