
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

        private GenericRepository<Person> _personRepository;
        private GenericRepository<Farm> _farmRepository;
        private GenericRepository<FarmCrop> _farmCropRepository;
        private GenericRepository<FarmAddress> _farmAddressRepository;
        private GenericRepository<Crop> _cropRepository;
        private GenericRepository<Measurement> _measurementRepository;
        private GenericRepository<Pricing> _pricingtRepository;
        private GenericRepository<Planting> _plantingRepository;
        private GenericRepository<HarvestPeriod> _harvestPeriodRepository;
        private GenericRepository<LocalGovernment> _localGovernmentRepository;
        private GenericRepository<State> _stateRepository;




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

        public GenericRepository<Pricing> PricingRepository
        {
            get
            {
                if (this._pricingtRepository == null)
                {
                    this._pricingtRepository = new GenericRepository<Pricing>(_context);
                }

                return _pricingtRepository;
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

        public GenericRepository<Crop> CropRepository
        {
            get
            {
                if (this._cropRepository == null)
                {
                    this._cropRepository = new GenericRepository<Crop>(_context);
                }

                return _cropRepository;
            }
        }

        public GenericRepository<FarmAddress> FarmAddressRepository
        {
            get
            {
                if (this._farmAddressRepository == null)
                {
                    this._farmAddressRepository = new GenericRepository<FarmAddress>(_context);
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

        public GenericRepository<Person> PersonRepository
        {
            get
            {
                if (this._personRepository == null)
                {
                    this._personRepository = new GenericRepository<Person>(_context);
                }

                return _personRepository;
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
