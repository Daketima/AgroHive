
using FarmMartDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmMartDAL.Implementation
{
    public class UnitOfWork : IDisposable
    {
        private readonly FarmerMartEntities _context = new FarmerMartEntities();

        private GenericRepository<AspNetUsers> _personRepository;
        private GenericRepository<Farm> _farmRepository;
        private GenericRepository<FarmCrop> _farmCropRepository;
        private GenericRepository<Address> _farmAddressRepository;
        private GenericRepository<CropVariety> _cropRepository;
        private GenericRepository<Measurement> _measurementRepository;
        private GenericRepository<CropPrice> _CropPricetRepository;
        private GenericRepository<Planting> _plantingRepository;
        private GenericRepository<HarvestPeriod> _harvestPeriodRepository;
        private GenericRepository<LocalGovernment> _localGovernmentRepository;
        private GenericRepository<State> _stateRepository;
        private GenericRepository<LivestockType> _livestockTypeRepository;
        private GenericRepository<Livestock> _livestockRepository;
        private GenericRepository<FarmLivestock> _farmLivestockRepository;
        private GenericRepository<LivestockPrice> _LivestockPriceRepository;
        private GenericRepository<Messaging> _messageRepository;
        private GenericRepository<MessageReply> _messageReplyRepository;
        private GenericRepository<CropVariety> _cropVarietyReplyRepository;
        private GenericRepository<LivestockBreed> _livestockBreedReplyRepository;
        private GenericRepository<AnimalGender> _animalGenderRepository;




        public GenericRepository<AnimalGender> AnimalGenderRepository
        {
            get
            {
                if (this._animalGenderRepository == null)
                {
                    this._animalGenderRepository = new GenericRepository<AnimalGender>(_context);
                }

                return _animalGenderRepository;
            }
        }

        public GenericRepository<LivestockBreed> LivestockBreedRepository
        {
            get
            {
                if (this._livestockBreedReplyRepository == null)
                {
                    this._livestockBreedReplyRepository = new GenericRepository<LivestockBreed>(_context);
                }

                return _livestockBreedReplyRepository;
            }
        }

        public GenericRepository<CropVariety> CropVarietyReplyRepository
        {
            get
            {
                if (this._cropVarietyReplyRepository == null)
                {
                    this._cropVarietyReplyRepository = new GenericRepository<CropVariety>(_context);
                }

                return _cropVarietyReplyRepository;
            }
        }

        public GenericRepository<MessageReply> MessageReplyRepository
        {
            get
            {
                if (this._messageReplyRepository == null)
                {
                    this._messageReplyRepository = new GenericRepository<MessageReply>(_context);
                }

                return _messageReplyRepository;
            }
        }


        public GenericRepository<Messaging> MessagingRepository
        {
            get
            {
                if (this._messageRepository == null)
                {
                    this._messageRepository = new GenericRepository<Messaging>(_context);
                }

                return _messageRepository;
            }
        }

        public GenericRepository<LivestockPrice> LivestockPriceRepository
        {
            get
            {
                if (this._LivestockPriceRepository == null)
                {
                    this._LivestockPriceRepository = new GenericRepository<LivestockPrice>(_context);
                }

                return _LivestockPriceRepository;
            }
        }

        public GenericRepository<FarmLivestock> FarmLivestockRepository
        {
            get
            {
                if (this._farmLivestockRepository == null)
                {
                    this._farmLivestockRepository = new GenericRepository<FarmLivestock>(_context);
                }

                return _farmLivestockRepository;
            }
        }

        public GenericRepository<Livestock> LivestockRepository
        {
            get
            {
                if (this._livestockRepository == null)
                {
                    this._livestockRepository = new GenericRepository<Livestock>(_context);
                }

                return _livestockRepository;
            }
        }

        public GenericRepository<LivestockType> LivestockTypeRepository
        {
            get
            {
                if (this._livestockTypeRepository == null)
                {
                    this._livestockTypeRepository = new GenericRepository<LivestockType>(_context);
                }

                return _livestockTypeRepository;
            }
        }

        public GenericRepository<State> StateRepository
        {
            get
            {
                if (this._stateRepository == null)
                {
                    this._stateRepository = new GenericRepository<State>(_context);
                }

                return _stateRepository;
            }
        }

        public GenericRepository<LocalGovernment> LocalGovernmentRepository
        {
            get
            {
                if (this._localGovernmentRepository == null)
                {
                    this._localGovernmentRepository = new GenericRepository<LocalGovernment>(_context);
                }

                return _localGovernmentRepository;
            }
        }

        public GenericRepository<HarvestPeriod> HarvestPeriodRepository
        {
            get
            {
                if (this._harvestPeriodRepository == null)
                {
                    this._harvestPeriodRepository = new GenericRepository<HarvestPeriod>(_context);
                }

                return _harvestPeriodRepository;
            }
        }


        public GenericRepository<Planting> PlantingRepository
        {
            get
            {
                if (this._plantingRepository == null)
                {
                    this._plantingRepository = new GenericRepository<Planting>(_context);
                }

                return _plantingRepository;
            }
        }

        public GenericRepository<CropPrice> CropPriceRepository
        {
            get
            {
                if (this._CropPricetRepository == null)
                {
                    this._CropPricetRepository = new GenericRepository<CropPrice>(_context);
                }

                return _CropPricetRepository;
            }
        }

        public GenericRepository<Measurement> MeasurementRepository
        {
            get
            {
                if (this._measurementRepository == null)
                {
                    this._measurementRepository = new GenericRepository<Measurement>(_context);
                }

                return _measurementRepository;
            }
        }

        public GenericRepository<FarmCrop> FarmCropRepository
        {
            get
            {
                if (this._farmCropRepository == null)
                {
                    this._farmCropRepository = new GenericRepository<FarmCrop>(_context);
                }

                return _farmCropRepository;
            }
        }

        public GenericRepository<CropVariety> CropRepository
        {
            get
            {
                if (this._cropRepository == null)
                {
                    this._cropRepository = new GenericRepository<CropVariety>(_context);
                }

                return _cropRepository;
            }
        }

        public GenericRepository<Address> AddressRepository
        {
            get
            {
                if (this._farmAddressRepository == null)
                {
                    this._farmAddressRepository = new GenericRepository<Address>(_context);
                }

                return _farmAddressRepository;
            }
        }

        public GenericRepository<Farm> FarmRepository
        {
            get
            {
                if (this._farmRepository == null)
                {
                    this._farmRepository = new GenericRepository<Farm>(_context);
                }

                return _farmRepository;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
