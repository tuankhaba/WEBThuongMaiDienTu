   ---trigger: Cửa hàng không được mua sản phẩm tại cửa hàng của chính mình
   Create trigger CuaHangKhongDuocMuaSanPhamCuaChinhMinhGioHang
   on ChiTietGioHang
   after insert, update
   as
   begin
		declare @MaCH Char(10), @ID char(10)
		select @MaCH = MaCuaHang, @ID = ID from inserted i, ChiTietKichCo kc, ChiTietPhanLoai p, SanPham sp 
											where i.MaCTKichCo = kc.MaCTKichCo and kc.MaCTPhanLoai = p.MaCTPhanLoai and p.MaSP = sp.MaSP
		if(@MaCH LIKE @ID)
			begin
				print N'Bạn không thể mua sản phẩm từ cửa hàng của chính bạn'
				rollback transaction
			end
   end
   go

   create trigger CuaHangKHongDuocMuaSanPhamCuaChinhMinhDonHang
   on DonHang
   after insert, update
   as
   begin
		declare @MaCH Char(10), @ID char(10)
		select @MaCH = MaCuaHang, @ID = ID from inserted i, ThongTinLienHe lh where i.MaLH = lh.MaLH
		if(@MaCH LIKE @ID)
			begin
				print N'Bạn không thể mua sản phẩm từ cửa hàng của chính bạn'
				rollback transaction
			end
   end
   go



      select * from ChiTietGioHang
   update SanPham set Xoa = 0 
   exec XoaSanPham 'ND001SP013'

insert into NguoiDung(ID,TenNguoiDung,GioiTinh,Email,NgaySinh) values ('ND001',N'Lê Phát Đạt',N'Nam','Datlvnttan@gmail.com','04/24/2002')
insert into NguoiDung(ID,TenNguoiDung,GioiTinh,Email,NgaySinh) values ('ND002',N'Nguyễn Thị Mỹ Duyên',N'Nữ','LePhatDat233@gmail.com','02/26/2002')



insert into TaiKhoan values('Datlvnttan','12345678','1','ND001')
insert into TaiKhoan values('MyDuyenXD','26022002','1','ND002')

select * from NguoiDung nd, TaiKhoan where nd.ID = TaiKhoan.ID




insert into Tinh_ThanhPho values('t1',N'Bến Tre')
insert into Quan_Huyen values('q1','t1',N'Mỏ Cày Bắc')
insert into Xa_Phuong values('x1','q1',N'Tân Thành Bình')

insert into Tinh_ThanhPho values('t2',N'HCM')
insert into Quan_Huyen values('q2','t1',N'Mỏ Cày Nam')
insert into Quan_Huyen values('q3','t2',N'Gò vấp')


insert into Xa_Phuong values('x4','q2',N'tân Phú Tây')

insert into Xa_Phuong values('x5','q3',N'tân Phú Tây')

insert into Xa_Phuong values('x6','q2',N'giòng trôm')
insert into Xa_Phuong values('x2','q1',N'Lê trọng tấn')
insert into Xa_Phuong values('x3','q1',N'Tân Phú Tây')

select * from ChiTietGioHang



