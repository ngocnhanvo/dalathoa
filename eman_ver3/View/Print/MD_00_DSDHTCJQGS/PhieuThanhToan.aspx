<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhieuThanhToan.aspx.cs" Inherits="PrintControllers_MD_00_DSDHTCJQGS_PhieuThanhToan" %>

<!DOCTYPE html>
<html lang="vi">
<head>
    <meta charset="UTF-8">
    <title>Phiếu thanh toán</title>
    <script src="../../../js/Public_script/JsBarcode.all.min.js"></script>
    <style>
        body {
            width: 80mm;
            font-family: Arial, sans-serif; /* Giữ đúng font Arial bạn muốn */
            font-size: 13px;
            line-height: 1.4;
            margin: auto;
        }

        @media print {
            body {
                width: 80mm; /* in đúng vật lý */
                margin: 0;
            }
        }

        table {
            width: 100%;
            border-collapse: collapse;
        }

        td {
            padding: 4px 6px;
            vertical-align: top;
        }

        .center {
            text-align: center;
        }

        .right {
            text-align: right;
        }

        .bold {
            font-weight: bold;
        }

        .line-solid {
            border-bottom: 1px solid #000;
            padding: 1px;
        }

        .line {
            border-bottom: 1px dashed #000;
            padding: 1px;
        }

        .title {
            font-size: 20px;
            font-weight: bold;
        }

        .subtitle {
            font-size: 18px;
            font-weight: bold;
            padding-top: 10px;
        }

        .small {
            font-size: 90%;
        }

        #barcode {
            margin-top: 3px;
        }

        /* Nút bấm hiển thị trên màn hình */
        .btn-print {
            background-color: #e6e6e6;
            color: #000000;
            padding: 12px 24px;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            font-weight: bold;
            position: fixed;
            right: 50px;
        }

        .btn-print:hover {
            background-color: #fff6e7;
        }

        @media print {
            @page {
                margin: 0; /* Loại bỏ lề mặc định của trình duyệt */
            }
            body {
                width: 80mm; /* Khổ giấy K80 */
                margin: 0;
                padding: 5mm; /* Tạo khoảng thở hai bên lề */
            }
            .no-print {
                display: none; /* Ẩn các nút bấm không cần thiết khi in */
            }
        }
    </style>
</head>

<body>
    <div class="no-print" style="text-align: center; margin: 20px 0;">
        <button onclick="printOrder()" class="btn-print">
            🖨️ In
        </button>
    </div>
    <table>
        <!-- HEADER -->
        <tr>
            <td colspan="4" class="center title">
                ĐÀ LẠT HOA
            </td>
        </tr>
        <tr>
            <td colspan="4" class="center">Linh Xuân, Thủ Đức, TP. HCM</td>
        </tr>
        <tr><td colspan="4" class="line-solid"></td></tr>
        <tr>
            <td colspan="4" class="center subtitle">PHIẾU THANH TOÁN</td>
        </tr>

        <tr>
            <td colspan="4">Thông tin nhân viên</td>
        </tr>
        <tr>
            <td colspan="4">
                <table>
                    <tr>
                        <td style="width: 65px;">Số CT:</td>
                        <td><%=dtPublic.Rows[0]["sophieu"] %></td>
                    </tr>
                    <tr>
                        <td>Ngày CT:</td>
                        <td><%=dtPublic.Rows[0]["ngayCT"] %></td>
                    </tr>
                    <tr>
                        <td>Nhân viên:</td>
                        <td><%=dtPublic.Rows[0]["nhanvien"] %></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr><td colspan="4" class="line"></td></tr>

        <tr>
            <td colspan="4">Thông tin người mua</td>
        </tr>
        <tr>
            <td colspan="4">
                <table>
                    <tr>
                        <td style="width: 65px;">Tên:</td>
                        <td><%=dtPublic.Rows[0]["ten_khachhang"] %></td>
                    </tr>
                    <tr>
                        <td>SĐT:</td>
                        <td><%=dtPublic.Rows[0]["tel"] %></td>
                    </tr>
                    <tr>
                        <td>Địa chỉ:</td>
                        <td><%=dtPublic.Rows[0]["diachi"] %></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr><td colspan="4" class="line"></td></tr>

        <tr>
            <td colspan="4">Thông tin người nhận</td>
        </tr>
        <tr>
            <td colspan="4">
                <table>
                    <tr>
                        <td style="width: 65px;">Tên:</td>
                        <td><%=dtPublic.Rows[0]["ten_nguoinhan"] %></td>
                    </tr>
                    <tr>
                        <td>SĐT:</td>
                        <td><%=dtPublic.Rows[0]["sdt_nguoinhan"] %></td>
                    </tr>
                    <tr>
                        <td>Địa chỉ:</td>
                        <td><%=dtPublic.Rows[0]["diachi_nguoinhan"] %></td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr><td colspan="4" class="line"></td></tr>

        <tr>
            <td colspan="4">Thông tin đơn hàng</td>
        </tr>
        <tr>
            <td colspan="4">
                <table>
                    <tr>
                        <td style="width: 105px;">Ngày giao:</td>
                        <td><%=dtPublic.Rows[0]["ngaygiao"] %></td>
                    </tr>
                    <tr>
                        <td>Trạng thái T.Toán:</td>
                        <td><%=dtPublic.Rows[0]["trangthaithanhtoan"] %></td>
                    </tr>
                    <tr>
                        <td>Ghi chú:</td>
                        <td><%=dtPublic.Rows[0]["ghichu"] %></td>
                    </tr>
                </table>
            </td>
        </tr>
        <!-- HEADER BẢNG -->
        <tr class="center bold">
            <td colspan="2">Tên hàng và SL</td>
            <td class="right">Giá bán</td>
            <td class="right">Thành tiền</td>
        </tr>

        <!-- DÒNG HÀNG -->
        <%foreach (System.Data.DataRow row in dtPublic.Rows) { %>
        <tr>
            <td colspan="4"><%=row["mota_tiengviet"] %></td>
        </tr>
        <tr>
            <td colspan="2" class="center"><%=row["soluongStr"] %></td>
            <td class="right"><%=row["giabanStr"] %></td>
            <td class="right"><%=row["thanhtienStr"] %></td>
        </tr>
        <%} %>
        <!-- LOOP TỚI ĐÂY -->

        <tr>
            <td colspan="4">
                <div class="line"></div>
            </td>
        </tr>

        <!-- TỔNG TIỀN -->
        <tr>
            <td colspan="3" class="right bold">Tổng tiền:</td>
            <td class="right"><%=dtPublic.Rows[0]["tongtienStr"] %></td>
        </tr>

        <tr>
            <td colspan="4"></td>
        </tr>

        <tr>
            <td colspan="3" class="right bold">Thanh toán:</td>
            <td class="right"><%=dtPublic.Rows[0]["da_thanhtoanStr"] %></td>
        </tr>

        <tr>
            <td colspan="3" class="right"><span class="small">(Đã làm tròn)</span></td>
            <td class="right"></td>
        </tr>

        <tr>
            <td colspan="4">
                <div class="line"></div>
            </td>
        </tr>

        <!-- TIỀN -->
        <tr>
            <td colspan="3" class="right">Tiền mặt:</td>
            <td class="right"><%=dtPublic.Rows[0]["tienmatStr"] %></td>
        </tr>
        <tr>
            <td colspan="3" class="right">Thối lại:</td>
            <td class="right"><%=dtPublic.Rows[0]["thoilaiStr"] %></td>
        </tr>

        <tr>
            <td colspan="4" class="right">
                <span class="small">(Giá trên đã bao gồm thuế GTGT)</span>
            </td>
        </tr>

        <!-- BARCODE -->
        <tr><td colspan="4" class="line-solid"></td></tr>
        <tr>
            <td colspan="4" class="center">
                <svg id="barcode"></svg>
            </td>
        </tr>
        <tr><td colspan="4" class="line-solid"></td></tr>
        <!-- FOOTER -->
        <tr>
            <td colspan="4" class="center small">
                Quý khách vui lòng kiểm tra lại hàng hóa trước khi thanh toán, xin cảm ơn!
            </td>
        </tr>

    </table>
    <script type="text/javascript">
        JsBarcode("#barcode", "<%=dtPublic.Rows[0]["barcode_sophieu"] %>", {
            format: "CODE128",     // Chuẩn mã vạch bạn đang dùng
            width: 2,              // Độ rộng của từng vạch đơn
            height: 60,            // Chiều cao barcode
            displayValue: false,    // Hiển thị dòng chữ DHB000002 bên dưới vạch
            fontSize: 18,
            font: "Arial",         // Đồng bộ font Arial cho toàn phiếu
            margin: 10
        });

        function printOrder() {
            // Nếu bạn dùng JsBarcode, hãy đảm bảo barcode đã render xong trước khi gọi lệnh này
            window.print();
        }
    </script>
</body>
</html>
