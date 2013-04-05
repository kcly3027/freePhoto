using System;
using System.Collections.Generic;
using System.Text;

namespace freePhoto.Web
{
    /// <summary>
    ///  生成唯一主键
    /// </summary>
    public class IdentityGenerator
    {
        static long lastIdentity = 0;
        static IdentityGenerator o = new IdentityGenerator( );
        /// <summary>
        ///  实例化
        /// </summary>
        public static IdentityGenerator Instance
        {
            get
            {
                if(o != null)
                    return o;
                else
                    return new IdentityGenerator( );
            }
        }
        /// <summary>
        ///  下一个主键值
        /// </summary>
        public string NextIdentity( )
        {
            long idint = DateTime.Now.Ticks - new DateTime( 2000, 1, 1 ).Ticks;
            while(lastIdentity >= idint) idint = lastIdentity + 1;
            lastIdentity = idint;

            return Convert.ToString( idint, 16 );
        }
        public long NextId()
        {
            long idint = DateTime.Now.Ticks - new DateTime(2000, 1, 1).Ticks;
            while (lastIdentity >= idint) idint = lastIdentity + 1;
            lastIdentity = idint;

            return idint;
        }
    }//class end
}