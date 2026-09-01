using System.ComponentModel;

namespace eNumPB
{
    public enum PathImage
    {
        [Description("images/application/productType")]
        NhomHHVT,
        [Description("images/application/product")]
        HHVT,
		[Description("images/application/user")] //add
        USER,
        [Description("images/application/product")] //add
        SANPHAM,
        [Description("images/application/partner")] //add
        DOITAC,
        [Description("images/icon/notfound.svg")]
        ImageNotFound
    };

    public enum PathFile
    {
        [Description("images/application/report")]
        Report
    };
}