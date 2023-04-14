# AspNet MVC

## Logger
- Dich vu Logger mac dinh duoc dang ky vao ConfigureServices(AddTransient)
- Co 6 cap do Logger
- Serilog
## Action
## View
- View(template): Doc .cshtml(template-duong dan tuyet doi toi .cshtml), tra ve ViewResult
- View(template,model): Truyen model sang View
- View("xinchao",model): Tim trong View/Controller(Name)/xinchao.cshtml
- View(): Doc .cshtml trung ten voi Action cua Controller -> Muon truyen model thi truyen View(model);
- Muon goi den cau truc View khac View/Controller/Action :
	+ services.Configure<RazorViewEngineOptions>(options =>
			{
				// {0} Ten Action
				// {1} Ten Controller
				// {2} Ten Area
				options.ViewLocationFormats.Add("/MyView/{1}/{0}"+RazorViewEngine.ViewExtension );
			});
## Truyen du lieu tu Controller sang View:
- Su dung View(Model)	
- ViewData: 
- ViewBag: Doi tuong dynamic
## Truyen du lieu tu trang nay sang trang khac:
- TempData: Su dung section he thong de luu du lieu va trang khac co the doc duoc du lieu nay. Khi doc ra lan dau tien thi du lieu duoc xoa luon.
- Su dung:
	+ TemData["Key"]= "Value";
	+ Xay dung Key la property co chi thi [TempData] o tren dau

## Route
### StatusCodePagesMiddleware
- Tao phuong thuc mo rong xu ly cac error tu 400-599
### Anh xa Url vao Controller
- Mac dinh: /controller/action
- Thiet lap Url bang [Route("....")]
- Khi thiet lap Route o Controller thi phai thiet lap Route o Action. Neu khong muon thi: [Route("he-mat-troi/[action]")]
- [HttpGet("/saomoc/html")]...
### Area
- La ten  dung de Routing
- La cau truc thu muc chua MVC
- Thiet lap Area cho Controller bang [Area("Area Name")]
- Su dung endpoint.MapAreaControllerRoute() de anh xa Url vao Controller
- View luu tru o: Areas/Area-Name/Views/Controller-Name/Action.cshtml