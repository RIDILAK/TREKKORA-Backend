using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public interface IGuideProfileService
    {
        Task<Responses<List<GuideDto>>> GetAllGuides();
        Task<Responses<GuideDto>> GetByIdGuides(Guid id);
        Task<Responses<string>> AddProfile(GuideProfileDto guideProfile, IFormFile formFile, IFormFile formFile1, Guid id);

        Task<Responses<string>> UpdateProfile(Guid id, GuideDto guideDto, IFormFile formFile, IFormFile formFile1);

        Task<Responses<string>> DeleteProfile(Guid id);
        Task<Responses<string>> ToggleBlockGuide(Guid id);


    }
    public class GuidProfileService : IGuideProfileService
    {
        private readonly IGuidProfileRepositories _guideProfilerepository;
        private readonly IGuidRepository _guidRepository;
        private readonly ICLoudinaryServices _cloudinaryServices;
        private readonly IMapper _mapper;

        public GuidProfileService(ICLoudinaryServices cLoudinaryServices, IGuidRepository guidRepository, IGuidProfileRepositories guidProfileRepositories, IMapper mapper)
        {
            _cloudinaryServices = cLoudinaryServices;
            _guidRepository = guidRepository;
            _guideProfilerepository = guidProfileRepositories;
            _mapper = mapper;
        }

        public async Task<Responses<List<GuideDto>>> GetAllGuides()
        {
            var result = await _guideProfilerepository.GetAllGuidesAsync();
            var guides = _mapper.Map<List<GuideDto>>(result);

            return new Responses<List<GuideDto>> { Data = guides, Message = "Fetched Guides", StatuseCode = 200 };

        }

        public async Task<Responses<string>> AddProfile(GuideProfileDto guideProfile, IFormFile formFile, IFormFile formFile1, Guid id)
        {
            string uploadedProfileImage = await _cloudinaryServices.UploadImage(formFile);
            string uploadedCertificateImage = await _cloudinaryServices.UploadImage(formFile1);
            var guides = _mapper.Map<GuideProfile>(guideProfile);
            guides.ProfileImage = uploadedProfileImage;
            guides.Certificates = uploadedCertificateImage;
            guides.Id = Guid.NewGuid();
            guides.GuideId = id;
            await _guidRepository.AddAsync(guides);
            return new Responses<string> { Message = "Guide Profile upload Succesfully", StatuseCode = 200 };
        }

        public async Task<Responses<GuideDto>> GetByIdGuides(Guid id)
        {
            var guide = await _guideProfilerepository.GetByIdAsync(id);
            if (guide == null)
            {
                return new Responses<GuideDto> { Message = "Id not Found", StatuseCode = 401 };
            }

            var guideDto = _mapper.Map<GuideDto>(guide);
            return new Responses<GuideDto> { StatuseCode = 200, Data = guideDto, Message = "Guide Fetched" };


        }

     public  async Task<Responses<string>> UpdateProfile(Guid id, GuideDto guideDto, IFormFile formFile,IFormFile formFile1)
        {
            var guide = await _guideProfilerepository.GetByIdAsync(id);
            if (guide == null)
            {
                return new Responses<string> { Message = "Id not Found", StatuseCode = 200 };

            }
            string uploadedProfileImage = await _cloudinaryServices.UploadImage(formFile);
            string uploadedCertificateImage = await _cloudinaryServices.UploadImage(formFile1);


            guide.Name = guideDto.Name;
            guide.Email = guideDto.Email;
            guide.GuideProfile.ProfileImage= uploadedProfileImage;
            guide.GuideProfile.Mobile=guideDto.GuideProfileDto.Mobile;
            guide.GuideProfile.Location=guideDto.GuideProfileDto.Location;
            guide.GuideProfile.Experience=guideDto.GuideProfileDto.Experience;
            guide.GuideProfile.Languages=guideDto.GuideProfileDto.Languages;
            guide.GuideProfile.AreasCovered=guideDto.GuideProfileDto.AreasCovered;
            guide.GuideProfile.Certificates= uploadedCertificateImage;
            guide.GuideProfile.Bio=guide.GuideProfile.Bio;
            guide.GuideProfile.WhyTravelWithMe=guide.GuideProfile.WhyTravelWithMe;

            await _guideProfilerepository.UpdateAsync(guide);
            return new Responses<string> { Message = "Profile Updated Succesfully", StatuseCode = 200 };
        }

        public async Task<Responses<string>> DeleteProfile(Guid id)
        {
            var guide = await _guideProfilerepository.GetByIdAsync(id);

            if (guide == null)
            {
                return new Responses<string> { Message = "Id not Found", StatuseCode = 200 };

            }
            await _guideProfilerepository.DeleteAsync(guide);
            return new Responses<string> { Message = "Guide Deleted Succesfully" ,StatuseCode = 200 };
        }

        public async Task<Responses<string>> ToggleBlockGuide(Guid id)
        {
            var guide = await _guideProfilerepository.GetByIdAsync(id);
            if (guide == null)
                return new Responses<string> { StatuseCode = 404, Message = "Guide not found" };

            guide.IsBlocked = !guide.IsBlocked;
            await _guideProfilerepository.UpdateAsync(guide);

            string status = guide.IsBlocked ? "blocked" : "unblocked";
            return new Responses<string> { StatuseCode = 200, Message = $"Guide {status} successfully" };
        }
    }
}
