using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjCompany.Models;
using prjCompany.ViewModels;

namespace prjCompany.Controllers
{
    public class HomeController : Controller
    {
        //GET:Home
        //建立dbEmployeeEntities類別物件db，可用來存取dbEmployee.mdf資料庫
        dbEmployeeEntities db = new dbEmployeeEntities();

        //指定Index動作方法的depId參數值預設為1
        public ActionResult Index(int depId = 1)
        {
            ViewBag.DepName = db.tDeparment
                                                    .Where(m => m.fDepId == depId)
                                                    .FirstOrDefault().fDepName + "部門";

            //建立CVMDepEmp的ViewModel物件vm,並指定該物件的department屬性值為所有tDeparment資料表的所有紀錄
            //指定employee屬性值為depId參數所對應的tEmployee資料表的所有紀錄
            CVMDepEmp vm = new CVMDepEmp()
            {
                department = db.tDeparment.ToList(),
                employee = db.tEmployee
                                    .Where(m => m.fDepId == depId).ToList()
            };
            //將ViewModel物件vm傳到Index檢視
            return View(vm);
        }

        //GET:Home/Create
        //執行Home/Create連結呼叫Create動作方法，該方法會將tDeparment部門的串列物件傳送的Create檢視
        public ActionResult Create()
        {
            return View(db.tDeparment.ToList()); 
        }

        //Post:Home/Create
        //在Create檢視按下submit按鈕時會呼叫此動作方法，該方法會將指定的emp員工物件新增到tEmployee資料表內
        [HttpPost]
        public ActionResult Create(tEmployee emp)
        {
            try
            {
                db.tEmployee.Add(emp);
                db.SaveChanges();
                //呼叫Index動作方法同時傳入depId參數值為emp.fDepId，讓Index動作方可取到目前新增的員工的部門
                //使Index檢視可顯示目前部門的員工資料
                return RedirectToAction("Index", new { depId = emp.fDepId });
            }
            catch(Exception ex)
            {}
            return View(emp);
        }

        //GET:Home/Delete?fEmpId=value
        //呼叫Delete動作方法同時傳入fEmpId，並依fEmpId刪除tEmployee員工資料表中指定的紀錄
        public ActionResult Delete(string fEmpId)
        {
            var emp = db.tEmployee.Where(m => m.fEmpId == fEmpId).FirstOrDefault();
            db.tEmployee.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index", new { depId = emp.fDepId });
        }
    }
}