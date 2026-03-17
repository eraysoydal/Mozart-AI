using AutoMapper;
using CleanArchitecture.Core.Interfaces.Repositories;
using CleanArchitecture.Core.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Genres.Queries.GetAllGenres
{
    public class GetAllGenresQuery : IRequest<PagedResponse<GetAllGenresViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, PagedResponse<GetAllGenresViewModel>>
    {
        private readonly IGenreRepositoryAsync _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllGenresQueryHandler(
            IGenreRepositoryAsync categoryRepository, 
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetAllGenresViewModel>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllGenresParameter>(request);
            var result = await _categoryRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var viewModels = _mapper.Map<List<GetAllGenresViewModel>>(result);
            return new PagedResponse<GetAllGenresViewModel> (viewModels, validFilter.PageNumber, validFilter.PageSize); ;
        }
    }
}
