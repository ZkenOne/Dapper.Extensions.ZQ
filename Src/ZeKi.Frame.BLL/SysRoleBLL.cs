﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeKi.Frame.IBLL;
using ZeKi.Frame.IDAL;
using ZeKi.Frame.Model;

namespace ZeKi.Frame.BLL
{
    public class SysRoleBLL : BaseBLL, ISysRoleBLL
    {
        public SysRoleBLL(IBaseDAL baseDAL) : base(baseDAL)
        {

        }

        public string GetRoL()
        {
            return $"SysRoleBLL";
        }
    }
}
