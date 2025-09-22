namespace UserDataAccessService.Handlers.Commands.Images
{
    using MediatR;

    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, string>
    {
        public async Task<string> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            string filePath = null;
            if (request.Image != null && request.Image.Length > 0)
            {
                string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(request.Image.FileName);
                using (var stream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream);
                }

                filePath = $"/images/{fileName}";
            }

            return filePath;
        }
    }
}
