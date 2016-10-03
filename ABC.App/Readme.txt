Hướng dẫn xử dụng:
	1/ Đổi ConnectionString trong App.config của ABC.App và ABC.Database.
	
	2/ Mở TOOLS ->
		NuGet Packet Manager ->
			Packet Manager Console gõ:
				"Update-Database" để tạo Database + Seed Default Data trong sql theo ConnectionString.
				
	3/ Làm tiếp phần việc của mình với các lưu ý sau:
		Không đụng vào ABC.Database folder: Objects, ObjectContexts, Migrations
		MÚN THAY ĐỔI GÌ TRONG 3 FOLDER TRÊN THÌ LIÊN HỆ NGƯỜI CHIỆU TRÁCH NHIỆM