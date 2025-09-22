namespace UserDataAccessService.Handlers.Commands.Images
{
    using MediatR;
    
    using Microsoft.AspNetCore.Http;

    public class UploadImageCommand : IRequest<string>
    {
        public IFormFile Image { get; set; }
    }
}
