using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using exact.api.Data.Model;
using exact.api.Model.Proxy;
using exact.api.Repository;
using Microsoft.EntityFrameworkCore;

namespace exact.api.Business
{
    public class GroupActionBusiness : BaseBusiness<GroupActionEntity>
    {
        private readonly GroupActionRepository _groupActionRepository;
        private readonly IMapper _mapper;

        public GroupActionBusiness(GroupActionRepository groupActionRepository, IMapper mapper)
        {
            _groupActionRepository = groupActionRepository;
            _mapper = mapper;
        }


        public List<ActionProxy> GetFromGroupId(Guid groupId)
        {
            var actions = _groupActionRepository.GetAll().Include(i => i.Action)
                .Where(w => w.GroupId == groupId).Select(a => _mapper.Map<ActionEntity, ActionProxy>(a.Action)).OrderBy(o => o.Index).ToList();
            return actions;
        }
    }
}