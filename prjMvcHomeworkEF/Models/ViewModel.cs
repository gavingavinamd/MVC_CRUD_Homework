namespace prjMvcHomeworkEF.Models
{
    //2.1 搜尋條件（型別為額外定義的 class），包含最低價格、最高價格與評價（星星）
    public class ViewModel
    {
        public FoodSearchParams SearchParams { get; set; }
        public List<TblFood02> Foods { get; set; } //2.2 搜尋結果（型別為 List<TblFood>）

        public List<TblFoodOrder> SaveFoods { get; set; }



        //2.3 為 ViewModel 宣告建構式，然後在建構式中做 property 的初始化（整理記憶體中的房間）
        public ViewModel()
        {
           SearchParams = new FoodSearchParams();
           Foods = new List<TblFood02>();
           SaveFoods = new List<TblFoodOrder>();
        }
    }
 
    public class FoodSearchParams
    {
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public int? commentStar { get; set; }
    }
      

}
