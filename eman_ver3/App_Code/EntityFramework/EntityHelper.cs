//using Microsoft.EntityFrameworkCore;
//using DataAcess;
//public class EntityHelper
//{
//    public static EntityContext CreateDbContextCore()
//    {
//        InfoDB.Infomation info = new InfoDB.Infomation();
//        var builder = new DbContextOptionsBuilder<EntityContext>();
//        var connection = info.getConnectStr();
//        builder.UseSqlServer(connection);
//        return new EntityContext(builder.Options);
//    }
//}