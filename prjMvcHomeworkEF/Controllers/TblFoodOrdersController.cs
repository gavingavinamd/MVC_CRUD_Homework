using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjMvcHomeworkEF.Models;



namespace prjMvcHomeworkEF.Controllers
{
   


    public class TblFoodOrdersController : Controller
    {
        private readonly HomeworkDBContext _context;
        
        public TblFoodOrdersController(HomeworkDBContext context)
        {
            _context = context;
        }

        

        // GET: TblFoodOrders
        public async Task<IActionResult> Index()
        {
              return _context.TblFoodOrders != null ? 
                          View(await _context.TblFoodOrders.ToListAsync()) :
                          Problem("Entity set 'HomeworkDBContext.TblFoodOrders'  is null.");
        }
        

        // GET: TblFoodOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblFoodOrders == null)
            {
                return NotFound();
            }

            var tblFoodOrder = await _context.TblFoodOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblFoodOrder == null)
            {
                return NotFound();
            }

            return View(tblFoodOrder);
        }

        // GET: TblFoodOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblFoodOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,Foodld,OrderDateTime,PaidDateTime")] TblFoodOrder tblFoodOrder)
        {
            if (ModelState.IsValid)
            {
                

                _context.Add(tblFoodOrder);             
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblFoodOrder);
        }


/*
        public IActionResult PayFoodOrder(int cId,int orderId)
        {
            //1.確認這筆訂單在不在     搜尋
            var order = _context.TblFoodOrders.FirstOrDefault(x => x.Id == orderId);
            if (order == null)
            {
                return NotFound();
            }
            //2.確認這筆食物在不在
            var food = _context.TblFood02s.FirstOrDefault(x => x.Id == order.Foodld);
            if (food == null)
            {
                return NotFound();
            }
            //3.找顧客
            var customer = _context.TblCustomers.First(x => x.Id == order.CustomerId);     

           // var foodPrice = food.Price;
           // var cuMoney = customer.Money;
           // var substrationMoney = customer.Money - food.Price;
           
            order.PaidDateTime = DateTime.Now;
            customer.Money = customer.Money - food.Price;

            _context.Update(order);//更新整個資料列
            _context.Update(customer);

            _context.SaveChanges();
            return Ok();// OK http狀態
        }
*/

        //1.確認這筆訂單在不在
        //2.確認這筆食物在不在
        //3.找顧客
        public IActionResult PayFoodOrder(int cusId, int orderId)
        {
            var order = _context.TblFoodOrders.FirstOrDefault(x => x.Id == orderId);
            if(order == null)
            {
                return NotFound();
            }

            var food = _context.TblFood02s.FirstOrDefault(x => x.Id == order.Foodld);
            if(food == null)
            {
                return NotFound();
            }

            var customer = _context.TblCustomers.FirstOrDefault(x =>x.Id == order.CustomerId);
            if(customer == null)
            {
                return NotFound();
            }
            order.PaidDateTime = DateTime.Now;
            customer.Money = customer.Money - food.Price;

            try
            {
                if (customer.Money < 0)
                {
                    throw new Exception("我需要黃金!");
                }
                else
                {
                    _context.Update(order);
                    _context.Update(customer);
                    _context.SaveChanges();
                }             
            }
            catch (Exception ex)
            {
               ex.Message.ToString();              
            }

            //1.使用 join 在食物訂單列表的頁面，顯示顧客名字與餐點名稱        
          
            return Ok();
        }

        public IActionResult ShowOrderDetail()
        {
            var joinResult =
                from foodOrder in _context.TblFoodOrders
                join foodName in _context.TblFood02s on foodOrder.Foodld equals foodName.Id
                join cutomerName in _context.TblCustomers on foodOrder.CustomerId equals cutomerName.Id
                select new
                {
                    OrderId = foodOrder.Id,
                    FoodId = foodName.Id,
                    FoodName = foodName.Name,
                    CustomerId = cutomerName.Id,
                    CustomerName = cutomerName.Name,
                    OrderDateTime = foodOrder.OrderDateTime,
                    PaidDateTime = foodOrder.PaidDateTime
                };
            
            return View(joinResult);
        }


        // localhost:5012/TblFoodOrders/PayFoodOrder?cusId=1&orderId=12
        // TblFoodOrders/PayFoodOrder?cusId=1&orderId=12
        // TblFoodOrders/Index
        // TblCustomers/Index
        // TblFood02/Index
        //http://localhost:5012/TblFoodOrders/ShowOrderDetail

        // GET: TblFoodOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblFoodOrders == null)
            {
                return NotFound();
            }

            var tblFoodOrder = await _context.TblFoodOrders.FindAsync(id);
            if (tblFoodOrder == null)
            {
                return NotFound();
            }
            return View(tblFoodOrder);
        }

        // POST: TblFoodOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,Foodld,OrderDateTime,PaidDateTime")] TblFoodOrder tblFoodOrder)
        {
            if (id != tblFoodOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblFoodOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblFoodOrderExists(tblFoodOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblFoodOrder);
        }

        // GET: TblFoodOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblFoodOrders == null)
            {
                return NotFound();
            }

            var tblFoodOrder = await _context.TblFoodOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblFoodOrder == null)
            {
                return NotFound();
            }

            return View(tblFoodOrder);
        }

        // POST: TblFoodOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblFoodOrders == null)
            {
                return Problem("Entity set 'HomeworkDBContext.TblFoodOrders'  is null.");
            }
            var tblFoodOrder = await _context.TblFoodOrders.FindAsync(id);
            if (tblFoodOrder != null)
            {
                _context.TblFoodOrders.Remove(tblFoodOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblFoodOrderExists(int id)
        {
          return (_context.TblFoodOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
