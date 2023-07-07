Create database ThuongMaiDienTu
go
use ThuongMaiDienTu
go
CREATE TABLE NguoiDung
(
  ID VARCHAR(20) NOT NULL,
  TenNguoiDung NVARCHAR(50) NOT NULL,
  NickName NVARCHAR(50),
  AnhDaiDien TEXT,
  GioiTinh NVARCHAR(5),
  SoDienThoai CHAR(11),
  Email VARCHAR(50),	
  NgaySinh Date CHECK (NgaySinh < GETDATE()),
  PRIMARY KEY (ID)
);

CREATE TABLE TaiKhoan
(
  TenTaiKhoan VARCHAR(20) NOT NULL,
  MatKhau VARCHAR(120) NOT NULL,
  TrangThai BIT DEFAULT 0 NOT NULL, 
  ID VARCHAR(20) NOT NULL,
  PRIMARY KEY (TenTaiKhoan),
  FOREIGN KEY (ID) REFERENCES NguoiDung(ID)
);

CREATE TABLE LoaiSanPham
(
  MaLoai CHAR(10) NOT NULL,
  TenLoai NVARCHAR(50) NOT NULL,
  PRIMARY KEY (MaLoai)
);
CREATE TABLE ChiTietLoaiSanPham
(
	MaChiTietLoai CHAR(10) NOT NULL,
	MaLoai CHAR(10) NOT NULL,
	TenChiTiet NVARCHAR(50) NOT NULL,
	PRIMARY KEY (MaChiTietLoai),
	FOREIGN KEY (MaLoai) REFERENCES LoaiSanPham(MaLoai)
)

CREATE TABLE Tinh_ThanhPho
(
	MaTinh CHAR(2),
	TenTinh NVARCHAR(50)
	PRIMARY KEY (MaTinh)
);

CREATE TABLE Quan_Huyen
(
	MaQH CHAR(3),
	MaTinh CHAR(2),
	TenHuyen NVARCHAR(50),
	PRIMARY KEY (MaQH),
	FOREIGN KEY (MaTinh) REFERENCES Tinh_ThanhPho(MaTinh)
);
CREATE TABLE Xa_Phuong
(
	MaXP CHAR(5),
	MaQH CHAR(3),
	TenXa NVARCHAR(50),	
	PRIMARY KEY (MaXP),
	FOREIGN KEY (MaQH) REFERENCES Quan_Huyen(MaQH)
);

CREATE TABLE ThongTinLienHe
(
  MaLH VARCHAR(20),
  ID VARCHAR(20),
  TenNguoiNhan NVARCHAR(50),
  MaXP CHAR(5),
  DiaChiCuThe NVARCHAR(70) NOT NULL,
  SoDienThoai CHAR(10) NOT NULL,
  LoaiDiaChi NVARCHAR(10) NOT NULL,
  MacDinh bit,
  An BIT DEFAULT 0,
  PRIMARY KEY (MaLH),
  FOREIGN KEY (ID) REFERENCES NguoiDung(ID),
  FOREIGN KEY (MaXP) REFERENCES Xa_Phuong(MaXP)
);

CREATE TABLE CuaHang
(
  MaCuaHang VARCHAR(20) NOT NULL,
  TenCuaHang NVARCHAR(50) UNIQUE NOT NULL,
  AnhDaiDien TEXT,
  SoDienThoai CHAR(11),
  NgayDangKy DATE NOT NULL DEFAULT GETDATE(),
  DiaChiLayHang char(5) NOT NULL FOREIGN KEY REFERENCES Xa_Phuong(MaXP),
  ChuKy NVARCHAR(50),
  LuotTruyCap INT NOT NULL DEFAULT 0,
  TrangThai BIT NOT NULL DEFAULT 1,
  PRIMARY KEY (MaCuaHang),
  FOREIGN KEY (MaCuaHang) REFERENCES NguoiDung(ID)
);

CREATE TABLE SanPham
(
  MaSP VARCHAR(20) NOT NULL,
  TenSP NVARCHAR(300) NOT NULL,
  MaCuaHang VARCHAR(20) NOT NULL,
  AnhBia TEXT NOT NULL,
  MoTaSP NTEXT,
  DanhGia FLOAT DEFAULT 0 CHECK (DanhGia BETWEEN 0 and 5) NOT NULL,
  SoLuongDanhGia INT DEFAULT 0 NOT NULL,
  GiamGia FLOAT NOT NULL DEFAULT 0,
  ThoiHan DateTime,
  MaChiTietLoai CHAR(10) NOT NULL,
  Xoa Bit DEFAULT 0,
  TrangThai Bit DEFAULT 1,
  PRIMARY KEY (MaSP),
  FOREIGN KEY (MaCuaHang) REFERENCES CuaHang(MaCuaHang),
  FOREIGN KEY (MaChiTietLoai) REFERENCES ChiTietLoaiSanPham(MaChiTietLoai)
);

CREATE TABLE AnhSanPham
(
  MaAnh CHAR(20),
  MaSP VARCHAR(20) NOT NULL,
  DuongDanAnh TEXT NOT NULL,
  PRIMARY KEY (MaAnh),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);


CREATE TABLE ChiTietThongTinSP
(
  ThuocTinh NVARCHAR(50) NOT NULL,
  NoiDung NVARCHAR(100),
  MaSP VARCHAR(20) NOT NULL,
  PRIMARY KEY (ThuocTinh, MaSP),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

create TABLE ChiTietPhanLoai
(
  MaCTPhanLoai VARCHAR(30),
  TenPhanLoai NVARCHAR(50) NOT NULL,
  MaSP VARCHAR(20) NOT NULL,
  Gia INT NOT NULL, 
  Xoa Bit DEFAULT 0 NOT NUll,
  PRIMARY KEY (MaCTPhanLoai),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);

CREATE TABLE ChiTietKichCo
(
  MaCTKichCo VARCHAR(50),
  MaCTPhanLoai VARCHAR(30),
  KichCo NVARCHAR(50),
  SoLuong INT NOT NULL,
  Xoa Bit DEFAULT 0 NOT NUll,
  PRIMARY KEY (MaCTKichCo),
  FOREIGN KEY (MaCTPhanLoai) REFERENCES ChiTietPhanLoai(MaCTPhanLoai)
);

go
Create TABLE ChiTietGioHang
(
  ID VARCHAR(20) NOT NULL,
  MaCTKichCo VARCHAR(50),
  SoLuong INT NOT NULL,
  PRIMARY KEY (ID, MaCTKichCo),
  FOREIGN KEY (ID) REFERENCES NguoiDung(ID),
  FOREIGN KEY (MaCTKichCo) REFERENCES ChiTietKichCo(MaCTKichCo),
);
go

Create TABLE SanPhamYeuThich
(
  ID VARCHAR(20) NOT NULL,
  MaSP VARCHAR(20),
  PRIMARY KEY (ID, MaSP),
  FOREIGN KEY (ID) REFERENCES NguoiDung(ID),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP),
)

go
CREATE TABLE DanhGia
(
  NoiDung NVARCHAR(200),
  ID VARCHAR(20) NOT NULL,
  MaSP VARCHAR(20) NOT NULL,
  MucDanhGia INT CHECK(MucDanhGia BETWEEN 1 and 5) NOT NULL,
  NgayDanhGia DATETIME NOT NULL DEFAULT GetDate(),
  AnDanh bit not null DEFAULT 0,
  PRIMARY KEY (ID, MaSP),
  FOREIGN KEY (ID) REFERENCES NguoiDung(ID),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP)
);
CREATE TABLE KhuyenMai
(
  MaGiamGia VARCHAR(20) NOT NULL,
  TenKhuyenMai NVARCHAR(70),
  MaVoucher VARCHAR(30) NOT NULL,
  MaCuaHang VARCHAR(20) NOT NULL,
  TyLeGiamGia FLOAT NOT NULL CHECK(TyLeGiamGia BETWEEN 0 and 1),
  DonHangToiThieu INT NOT NULL,--Giá sản phẩm lớn hơn DieuKien
  LuotSuDung INT,
  MucGiamToiDa INT,
  NgayBatDau DATETIME,
  NgayKetThuc DATETIME NOT NULL,
  PRIMARY KEY (MaGiamGia),
  FOREIGN KEY (MaCuaHang) REFERENCES CuaHang(MaCuaHang)
);
alter table KhuyenMai
add Constraint chk_thoihan Check(NgayKetThuc > NgayBatDau)
go

CREATE TABLE DonHang
(
  MaDonHang VARCHAR(20) NOT NULL,
  MaCuaHang VARCHAR(20) NOT NULL,
  MaLH VARCHAR(20) FOREIGN KEY REFERENCES ThongTinLienHe(MaLH),
  NgayMua Date NOT NULL DEFAULT GETDATE(),
  MaVoucher VARCHAR(30),
  PhuongThucGiao NVARCHAR(12),
  PhuongThucThanhToan NVARCHAR(15),
  TrangThai NVARCHAR(50) NOT NULL CHECK(TrangThai in(N'Chờ xác nhận',N'Đang xử lý',N'Đang giao',N'Đã giao',N'Đã hủy',N'Bị từ chối')) DEFAULT N'Chờ xác nhận',
  ThanhTien BIGINT DEFAULT 0 NOT NULL,
  LoiNhan NVARCHAR(70),
  PRIMARY KEY (MaDonHang),
  FOREIGN KEY (MaCuaHang) REFERENCES CuaHang(MaCuaHang),
);


go
CREATE TABLE ChiTietDonHang
(
  MaDonHang VARCHAR(20) NOT NULL,
  MaSP VARCHAR(20) NOT NULL,
  MaCTKichCo VARCHAR(50),
  SoLuong INT NOT NULL,
  TongTien INT NOT NULL,
  PRIMARY KEY (MaDonHang,MaCTKichCo, MaSP),
  FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
  FOREIGN KEY (MaSP) REFERENCES SanPham(MaSP),
  FOREIGN KEY (MaCTKichCo) REFERENCES ChiTietKichCo(MaCTKichCo)
);

go
--Tự động cập nhật tổng tiền cho chi tiết đơn hàng
create trigger CapNhatTongTienCTDH
on ChiTietDonHang
after insert
as
begin
	declare @maDH char(30), @maCTKC char(50), @tlgiam int, @thoihan datetime, @giam int = 0, @gia int, @maCTCK char(50)
	select @maDH = MaDonHang, @tlgiam = s.GiamGia,@thoihan = ThoiHan, @gia = Gia, @maCTCK = i.MaCTKichCo from inserted i, SanPham s, ChiTietPhanLoai pl, ChiTietKichCo ct 
			where i.MaSP = s.MaSP and s.MaSP = pl.MaSP and i.MaCTKichCo = ct.MaCTKichCo and ct.MaCTPhanLoai = pl.MaCTPhanLoai
	if(@tlgiam>0 and @thoihan > GetDate())
		set @giam = @tlgiam/100*@gia
	update ChiTietDonHang set TongTien = (@gia - @giam)*SoLuong  where  MaDonHang = @maDH and MaCTKichCo = @maCTCK
end
go


--Chỉ có người từng mua sản phẩm đó, mới được quyền đánh giá
Create Trigger NguoiMuaMoiCoQuyenDanhGiaSP
on DanhGia
after insert
as
begin
	declare @id char(30), @masp char(30) 
	select @id = ID, @masp = MaSP from  inserted
	if(not exists(select * from ChiTietDonHang ct, DonHang d, ThongTinLienHe t 
				  where ct.MaDonHang = d.MaDonHang and d.MaLH = t.MaLH and MaSP = @masp and ID = @id)) 
	begin
		print N'Bạn không có quyền đánh giá khi bạn chưa mua sản phẩm này'
		rollback transaction
	end
end
go
--ràng buộc tự động tính mức đánh giá và số lượng đánh giá
Create Trigger CapNhatSoLuong_MucDanhGiaChoSanPham
on DanhGia
after insert
as
begin
	declare @id char(30), @masp char(30), @mucDG int
	select @id = ID, @masp = MaSP, @mucDG = MucDanhGia from  inserted
	update SanPham 
	    set DanhGia = (DanhGia*SoLuongDanhGia+@mucDG) / (SoLuongDanhGia+1), SoLuongDanhGia = SoLuongDanhGia +1 
	    where MaSP = @masp
end

go
--ràng buộc tự động giảm số lượng tồn của sản phẩm khi Đơn hàng đã ở trạng thái Đang xử lí
Create Trigger CapNhatSoLuongSPKhiDHChuyenTrangThaiDangXuLy 
on DonHang
after insert, update
as
begin
	declare @MaDH char(10), @TThai Nvarchar(30), @maVC char(30), @maCH char(20)
	select @MaDH = MaDonHang, @TThai = TrangThai, @maVC = MaVoucher, @maCH = MaCuaHang from  inserted
	if(@TThai Like N'Đang xử lý')
	begin
		update KhuyenMai set LuotSuDung = LuotSuDung -1 where MaVoucher = @maVC and MaCuaHang = @maCH
		declare ctdh cursor
		for select MaCTKichCo, SoLuong from ChiTietDonHang where MaDonHang = @MaDH
		open ctdh
		declare @mact char(20), @sl int
		Fetch next from ctdh into @mact, @sl
		while(@@FETCH_STATUS=0)
		begin
			update ChiTietKichCo set SoLuong = SoLuong - @sl where MaCTKichCo = @mact
			Fetch next from ctdh into @mact, @sl
		end	
		DEALLOCATE ctdh		
	end
end
go
SELECT * FROM ChiTietKichCo
--Ràng buộc khi đơn hàng đã ở trạng thái đang giao, và đã giao, thì sẽ ko đc hủy đơn hàng và cập nhật lại số lượng khi đơn hàng bị hủy thành công khi nó ở trạng thái đang xử lý:
create trigger TruongHopKhongDuocHuyDonHang
on DonHang
INSTEAD OF update
as
begin
	declare @TrangThaiMoi NVARCHAR(50), @MaDH char(20),@TrangThaiHT NVARCHAR(50), @maVC char(30), @maCH char(20)
	select @TrangThaiMoi = TrangThai, @MaDH = MaDonHang, @maVC = MaVoucher, @maCH = MaCuaHang from inserted
	select @TrangThaiHT = TrangThai from DonHang where MaDonHang = @MaDH 
	if(@TrangThaiMoi like N'Đã hủy' and @TrangThaiHT in(N'Đang giao',N'Đã giao'))
		print N'Bạn không thể hủy đơn hàng ở trạng thái "' +@trangThaiHT+ '"!!!'
	else
	begin
		update DonHang set MaCuaHang = i.MaCuaHang, MaLH = i.MaLH,NgayMua = i.NgayMua,
							MaVoucher = i.MaVoucher, TrangThai = i.TrangThai, LoiNhan = i.LoiNhan, ThanhTien = i.ThanhTien 
							from inserted i where DonHang.MaDonHang = @MaDH
		if(@TrangThaiMoi Like N'Đã hủy' and @TrangThaiHT like N'Đang xử lý')
		begin
		update KhuyenMai set LuotSuDung = LuotSuDung + 1 where MaVoucher = @maVC and MaCuaHang = @maCH
			declare ctdh cursor
			for select MaCTKichCo, SoLuong from ChiTietDonHang where MaDonHang = @MaDH
			open ctdh
			declare @mact char(20), @sl int
			Fetch next from ctdh into @mact, @sl
			while(@@FETCH_STATUS=0)
			begin
				update ChiTietKichCo set SoLuong = SoLuong + @sl where MaCTKichCo = @mact
				Fetch next from ctdh into @mact, @sl
			end	
			DEALLOCATE ctdh		
		end
	end
end


--select* from DonHang
--update DonHang set TrangThai = N'Đã hủy' where MaDonHang = 'ND002DH3  '

-- Ràng buộc: khi cập nhật 1 thông tin liên hệ là mặt định thì các thông tin liên hệ còn lại của tài khoản đó sẽ tự động chuyển trạng thái mặc định thành false;
go
   create trigger TuDongCapNhatThongTinLienHeMacDinh
   on ThongTinLienHe
   after insert
   as
   begin
		declare @macdinh bit, @id char(10), @maLh char(10), @an bit
		select @macdinh = inserted.MacDinh, @id = inserted.ID, @maLh= inserted.MaLH, @an = inserted.An from inserted
		if(@macdinh = 1)
		begin
			update ThongTinLienHe set MacDinh = 0 where ID = @id and  MacDinh = 1 and MaLH <>@maLh
		end
   end
   go
   create trigger TuDongCapNhatThongTinLienHeMacDinhKhiAn
   on ThongTinLienHe
   after update
   as
   begin
		declare @macdinh bit, @id char(10), @maLh char(10), @an bit
		select @macdinh = inserted.MacDinh, @id = inserted.ID, @maLh= inserted.MaLH, @an = inserted.An from inserted
		if(@macdinh = 1 )
		begin
			if(@an = 1)
			begin
				update ThongTinLienHe set MacDinh = 0 where MaLH = @maLh
				declare ttlh cursor
				for select MaLH, An from ThongTinLienHe where ID = @id
				open ttlh
				declare @lh char(10), @a bit
				Fetch next from ttlh into @lh, @a
				while(@@FETCH_STATUS=0)
				begin
					if(@a=0)
					begin
						update ThongTinLienHe set MacDinh = 1 where MaLH = @lh
						break;
					end
					Fetch next from ttlh into @lh, @a
				end	
				DEALLOCATE ttlh
			end
			else
			begin
				update ThongTinLienHe set MacDinh = 0 where ID = @id and  MacDinh = 1 and MaLH <>@maLh
			end
		end
		
   end
go
      --ràng buộc thông tin liên hệ là mặt định thì ko thể nào là trạng thái ẩn
   create trigger ThongTinLienHeMacDinhNotAn
   on ThongTinLienHe
   after insert
   as
   begin
		declare @macdinh bit, @id char(10), @an bit, @sl int, @maLh char(10)
		select @macdinh = inserted.MacDinh,@maLh = inserted.MaLH, @id = inserted.ID, @an= inserted.an from inserted
		select @sl = count(*) from ThongTinLienHe where ID = @id and MaLH <>@maLh and An = 0
		if((@macdinh=1 and @an =1) or (@sl =0 and @an=1))
		begin 
			Print(N'Một thông tin liên hệ mặc định không thể ở trạng thái ẩn')
			ROLLBACK TRANSACTION
		end
   end
      --Tự động làm mặc định cho thông tin liên hệ nếu tài khoản đó mới có 1 thông tin liên hệ
   go
   create trigger MotThongTinLienHeMacDinh
   on ThongTinLienHe
   after insert
   as
   begin
		declare @macdinh bit, @id char(10), @maLh char(10)
		select @macdinh = inserted.MacDinh, @id = inserted.ID, @maLh= inserted.MaLH from inserted
		if((select count(*) from ThongTinLienHe where ID = @id and MaLH <>@maLh and An = 0)=0)
		begin
			update ThongTinLienHe set MacDinh = 1 where MaLH = @maLh
		end
   end
   --Nếu xóa 1 thông tin liên hệ mặt định sẽ tự động cập nhật lại giá trị mặc định cho 1 trong những thông tin liên hệ còn lại (nếu còn)
   go
   Create trigger TuDongCapNhatKhiXoaThongTinLienHeMacDinh
   on ThongTinLienHe
   after delete
   as
   begin
		declare @macdinh bit, @id char(10)
		select @macdinh = deleted.MacDinh, @id = deleted.ID from deleted
		if(@macdinh = 1)
		begin
		declare ttlh cursor
		for select MaLH, An from ThongTinLienHe where ID = @id
		open ttlh
		declare @maLH char(10), @an bit
		Fetch next from ttlh into @maLH, @an
		while(@@FETCH_STATUS=0)
		begin
			if(@an=0)
			begin
				update ThongTinLienHe set MacDinh = 1 where MaLH = @maLH
				break;
			end
			Fetch next from ttlh into @maLH, @an
		end	
		DEALLOCATE ttlh
		end
   end
   --Một cửa hàng không thể chứa 2 mã Voucher giống nhau!!!
   go
   create trigger KhongTrungMaVoucherCungCuaHang
   on KhuyenMai
   after insert, update
   as
   begin
		declare @mavc varchar(30), @maCH char(10)
		select @mavc = inserted.MaVoucher , @maCH = inserted.MaCuaHang from inserted
		if((select count(*) from KhuyenMai where MaCuaHang = @maCH and MaVoucher = @mavc)>1)
		begin
			print(N'Một cửa hàng không thể chứa 2 mã Voucher giống nhau!!!');
			ROLLBACK TRANSACTION
		end
   end
   GO

    --Ràng buộc mã giảm giá phải tạm thời hợp lệ 
   create trigger MaGiamGiaPhaiHopLeVoiDonHang
   on DonHang
   after insert, update
   as
   begin
		declare @maVoucher Char(30), @mach char(30), @madh char(30),@hientai datetime = getdate()
		select @madh = MaDonHang, @maVoucher = MaVoucher, @mach = MaCuaHang from inserted
		--Kiểm tra xem mã giảm giá này có thuộc cửa hàng đó hay ko, và còn lượt sử dụng, có đang trong thời gian khuyến mãi hay ko
		if( @maVoucher is not null and not exists(select * from KhuyenMai where MaVoucher = @maVoucher and MaCuaHang = @mach and LuotSuDung > 0 and NgayBatDau <= @hientai and @hientai <= NgayKetThuc))
		begin
			print N'Mã giảm giá không hợp lệ, vui lòng kiểm tra lại!!!'
			rollback transaction
		end
   end


   go
   --Ràng buộc sản phẩm trong chi tiết đơn hàng phải thuộc về cửa hàng đó
      create trigger SanPhamChungMotDonHangPhaiChungMotCuaHang
   on ChiTietDonHang
   after insert, update
   as
   begin
		declare @MaSP Char(30), @MaCH CHAR(20)
		select @MaSP = MaSP, @MaCH = MaCuaHang from inserted i, DonHang d where i.MaDonHang = d.MaDonHang
		if(not exists(select * from SanPham where MaCuaHang = @MaCH and MaSP = @MaSP))
		begin
			print N'Có sản phẩm không nằm trong cửa hàng của đơn hàng mà bạn mua !!'
			rollback transaction
		end
   end
   go
   --Ràng buộc thêm sản phẩm vào giỏ hàng với số lượng phải nhỏ hơn số lượng tồn với sản phẩm đó
go
   create trigger SoLuongMuaPhaiCoDuSoLuongTon_GioHang
   on ChiTietGioHang
   after insert
   as
   begin
		declare @sl int, @MaCTKichCo char(20), @slton int
		select @MaCTKichCo = MaCTKichCo, @sl = SoLuong from inserted 
		select @slton = SoLuong from ChiTietKichCo where MaCTKichCo   = @MaCTKichCo
		if(@sl>@slton)
		begin
			print N'Số Lượng bạn mua đã vượt quá số lượng tồn !!'
			rollback transaction
		end
   end
   go
     create trigger SoLuongVuotQuaSoLuongTonSeDoiBangSoLuongTon_giohang_update
   on ChiTietGioHang
   INSTEAD OF update
   as
   begin
		declare @sl int, @MaCTKichCo char(20), @slton int, @id char(30)
		select @MaCTKichCo = MaCTKichCo, @sl = SoLuong, @id = ID from inserted 
		select @slton = SoLuong from ChiTietKichCo where MaCTKichCo   = @MaCTKichCo
		if(@sl>@slton)
			set @sl = @slton
		update  ChiTietGioHang set SoLuong = @sl where ID = @id and MaCTKichCo = @MaCTKichCo
   end
   go

      --Ràng buộc thêm sản phẩm vào giỏ hàng phải với số lượng phải nhỏ hơn số lượng tồn với đơn hàng
   create trigger SoLuongMuaPhaiCoDuSoLuongTon_DonHang
   on ChiTietDonHang
   after insert, update
   as
   begin
		declare @MaSP Char(30), @sl int, @MaCTKichCo char(20), @slton int
		select @MaSP = MaSP, @MaCTKichCo = MaCTKichCo, @sl = SoLuong from inserted
		select @slton = SoLuong from ChiTietKichCo where MaCTKichCo   = @MaCTKichCo
		if(@sl>@slton)
		begin
			print N'Số Lượng bạn mua đã vượt quá số lượng tồn !!'
			rollback transaction
		end
   end
   go
   --Ràng buộc phẩm trong chi tiết đơn hàng phải tồn tại MaCTKichCo của chính nó

   create trigger MaSPTonTaiMaCTKC
   on ChiTietDonHang
   after insert, update
   as
   begin
		declare @MaSP Char(30), @MaCTKichCo char(40)
		select @MaSP = MaSP, @MaCTKichCo = MaCTKichCo from inserted
		if(@MaCTKichCo not in (select MaCTKichCo 
								from ChiTietKichCo kc, ChiTietPhanLoai p 
								where kc.MaCTPhanLoai = p.MaCTPhanLoai and MaSP = @MaSP))
		begin
			print N'Mã sản phẩm '+ @MaSP +N' không có kích cở '+@MaCTKichCo
			rollback transaction
		end
   end
   go
   -- ràng buộc xóa chi tiết kích cỡ thành công nếu nó chưa từng được đặt hàng
   create trigger XoaCTKCNeuChuaTonTaiDonHang
	on ChiTietKichCo
	INSTEAD OF delete
	as
	begin
		declare @MaCTKC CHAR(40)
		select @MaCTKC = MaCTKichCo from deleted
		if(not exists(select * from ChiTietDonHang where MaCTKichCo = @MaCTKC))
		begin
			delete ChiTietGioHang where MaCTKichCo = @MaCTKC
			delete ChiTietKichCo where MaCTKichCo = @MaCTKC
		end
		else
		begin
			update ChiTietKichCo set Xoa = 1 where MaCTKichCo = @MaCTKC
		end
	
	end
	go	
	   -- ràng buộc xóa chi tiết phân loại thành công nếu nó chưa từng được đặt hàng
   create trigger XoaCTPLNeuChuaTonTaiDonHang
	on ChiTietPhanLoai
	INSTEAD OF delete
	as
	begin
		declare @MaCTPL CHAR(40)
		select @MaCTPL = MaCTPhanLoai from deleted
		if(not exists(select * from ChiTietDonHang ct, ChiTietKichCo kc where ct.MaCTKichCo = kc.MaCTKichCo and kc.MaCTPhanLoai = @MaCTPL))
		begin
			delete ChiTietPhanLoai where MaCTPhanLoai = @MaCTPL
		end
		else
		begin
			update ChiTietPhanLoai set Xoa = 1 where MaCTPhanLoai = @MaCTPL
		end
	
	end
	go
	create trigger ThemSanPhamVaoGioHang
	on ChiTietGioHang
	INSTEAD OF insert
	as
	begin
		declare @MaCTKichCo CHAR(50),@SoLuong int, @ID char(20)
		select @MaCTKichCo = MaCTKichCo, @SoLuong = SoLuong, @ID = ID from inserted
		if(exists(select * from ChiTietGioHang where ID = @ID and MaCTKichCo = @MaCTKichCo))
		begin		
			update ChiTietGioHang set SoLuong = SoLuong + @SoLuong	
									where ID = @ID  and MaCTKichCo = @MaCTKichCo
		end
	else
		begin
			insert into ChiTietGioHang (ID,MaCTKichCo,SoLuong) values (@ID,@MaCTKichCo,@SoLuong)
		end	
	end
	go
   --Thêm 1 sản phẩm vào giỏ hàng
   Create PROCEDURE ThemSPVaoGioHang(@ID CHAR(10), @MaCTKichCo CHAR(50), @SoLuong INT)
   AS 
   begin
	if(exists(select * from ChiTietGioHang where ID = @ID and MaCTKichCo = @MaCTKichCo))
		begin		
			update ChiTietGioHang set SoLuong = SoLuong + @SoLuong	
									where ID = @ID  and MaCTKichCo = @MaCTKichCo
		end
	else
		begin
			insert into ChiTietGioHang (ID,MaCTKichCo,SoLuong) values (@ID,@MaCTKichCo,@SoLuong)
		end
	end   
   go
      --xây dựng PROCEDURE để tự động xóa 1 sản phẩm nếu nó chưa từng đưuọc đặng mua, hoặc sẽ chuyển trạng thái ẩn nếu đã từng được mua    

   create PROCEDURE XoaSanPham(@MaSP as Char(30))
   AS 
   begin
	declare @sl int
	select @sl = COUNT(*) from ChiTietDonHang where MaSP = @MaSP
	if(@sl>0)
		begin
			update SanPham set Xoa = 1 where MaSP = @MaSP
		end
	else
		begin
			delete from ChiTietKichCo where MaCTKichCo in (select MaCTKichCo 
														from ChiTietKichCo kc, ChiTietPhanLoai pl
														where pl.MaCTPhanLoai = kc.MaCTPhanLoai and pl.MaSP = @MaSP)
			delete from ChiTietPhanLoai where MaSP = @MaSP
			delete from ChiTietGioHang where MaCTKichCo in (select MaCTKichCo from ChiTietKichCo kc, ChiTietPhanLoai pl where kc.MaCTPhanLoai = pl.MaCTPhanLoai and MaSP = @MaSP)
			delete from ChiTietThongTinSP where MaSP = @MaSP
			delete from AnhSanPham where MaSP = @MaSP
			delete from DanhGia where MaSP = @MaSP
			delete from SanPham where MaSP = @MaSP
		end
   end
   go


go
Create Proc XoaChiTietGioHangDuaVaoCTDonHang @id char(30), @maCTKC char(40), @sl int
as
begin
	declare @slgh int = 0
	select @slgh = SoLuong from ChiTietGioHang where ID = @id and MaCTKichCo = @maCTKC
	if(@slgh>0)
	begin
		if(@slgh >=@sl)
			delete from ChiTietGioHang where ID = @id and MaCTKichCo = @maCTKC
		else if(@sl<@slgh)
			UPDATE ChiTietGioHang set SoLuong = SoLuong - @sl where ID = @id and MaCTKichCo = @maCTKC
	end
end
go
Create Proc TinhGiaTriDonHangTruocKhuyenMai @Madh char(30), @gt int output
as
begin
		declare @BangGiamGia table
		(
			maSp char(10),
			Gia int,
			ThoiHan datetime,
			Giamgia floaT
		)
		insert into @BangGiamGia
		select sp.MaSP, Gia, ThoiHan, GiamGia from SanPham sp , ChiTietPhanLoai pl where sp.MaSP = pl.MaSP
		declare gg cursor
		for select MaSP, Gia, ThoiHan, Giamgia from @BangGiamGia
		open gg
		declare @maSP char(10),  @gia int,@thoihan Datetime, @giam float
		Fetch next from gg into @maSP, @gia, @thoihan,@giam
		while(@@FETCH_STATUS=0)
		begin
			if(@thoihan is not null and @thoihan >= getdate())
			begin
				print @maSP
				update @BangGiamGia set gia = Gia -convert(int,(@gia*@giam/100)) where maSp = @maSP
			end
			Fetch next from gg into @maSP, @gia, @thoihan,@giam
		end	
		DEALLOCATE gg
		set @gt = (select Sum(sp.Gia * ChiTietDonHang.SoLuong) from ChiTietDonHang, ChiTietKichCo, ChiTietPhanLoai, @BangGiamGia sp
				where ChiTietPhanLoai.MaCTPhanLoai = ChiTietKichCo.MaCTPhanLoai and ChiTietDonHang.MaCTKichCo = ChiTietKichCo.MaCTKichCo and sp.MaSP = ChiTietDonHang.MaSP
				and MaDonHang = @Madh)
	return 
end
go
create Proc ThemDanhGia @id char(20),@muc int, @noidung NVARCHAR(100), @maSP char(20), @an bit
as
begin
	insert into DanhGia values (@noidung, @id, @maSP, @muc,getdate(),@an)
end


select * from DonHang