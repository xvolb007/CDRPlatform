using AutoMapper;
using CDRPlatform.Domain.Dto.CallDetailRecordDtos;
using CDRPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDRPlatform.Domain.Mapping
{
    public class CallDetailRecordProfile : Profile
    {
        public CallDetailRecordProfile()
        {
            CreateMap<CallDetailRecord, CallDetailRecordDto>();
        }
    }
}
