﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DDD.HealthcareDelivery.Infrastructure.Prescriptions
{
    using Core.Mapping;
    using Core.Domain;
    using Domain.Prescriptions;
    using Core.Infrastructure.Data;

    public class PharmaceuticalPrescriptionRepository
        : EFRepository<PharmaceuticalPrescription, PharmaceuticalPrescriptionState, HealthcareContext>
    {

        #region Constructors

        public PharmaceuticalPrescriptionRepository(IObjectTranslator<PharmaceuticalPrescriptionState, PharmaceuticalPrescription> prescriptionTranslator,
                                                    IObjectTranslator<IDomainEvent, EventState> eventTranslator,
                                                    IAsyncDbContextFactory<HealthcareContext> contextFactory)
            : base(prescriptionTranslator, eventTranslator, contextFactory)
        {
        }

        #endregion Constructors

        #region Methods

        protected override IEnumerable<Expression<Func<PharmaceuticalPrescriptionState, object>>> RelatedEntitiesPaths()
        {
            yield return p => p.PrescribedMedications;
        }

        #endregion Methods

    }
}
