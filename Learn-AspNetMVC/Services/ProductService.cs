using Learn_AspNetMVC.Models;
using System.Collections.Generic;

namespace Learn_AspNetMVC.Services
{
	public class ProductService : List<ProductModel>
	{
		public ProductService() 
		{
			this.AddRange(new List<ProductModel>
			{
				new ProductModel {Id=1, Name="samsung",Price=1000},
				new ProductModel {Id=2, Name="iphone",Price=500},
				new ProductModel {Id=3, Name="nokia",Price=800},
				new ProductModel {Id=4, Name="huawei",Price=100},

			});
		}

	}
}
