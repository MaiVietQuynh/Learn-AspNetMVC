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
		+ 	services.Configure<RazorViewEngineOptions>(options =>
			{
				// {0} Ten Action
				// {1} Ten Controller
				// {2} Ten Area
				options.ViewLocationFormats.Add("/MyView/{1}/{0}"+RazorViewEngine.ViewExtension );
			});
## Truyen du lieu tu Controller sang View:
- Su dung View()	
- ViewData:
- ViewBag: