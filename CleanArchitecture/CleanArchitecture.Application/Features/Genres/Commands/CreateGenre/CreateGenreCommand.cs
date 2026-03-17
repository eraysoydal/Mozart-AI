using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Features.Genres.Commands.CreateGenre
{
    public class CreateGenreCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
    {
        private readonly IGenreRepositoryAsync _categoryRepositoryAsync;



        public CreateGenreCommandHandler(
            IGenreRepositoryAsync categoryRepositoryAsync)
        {
            _categoryRepositoryAsync = categoryRepositoryAsync;
        }


        public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var newGenre = new Genre
            {
                Name = request.Name,
                Description = request.Description
            };

            await _categoryRepositoryAsync.AddAsync(newGenre);

            return newGenre.Id;
        }
    }
}
