﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YurtApps.Application.DTOs.RoomDTOs
{
    public class GetRoomDto
    {
        public int RoomId { get; set; }
        public short RoomNumber { get; set; }
        public short RoomCapacity { get; set; }
    }
}
