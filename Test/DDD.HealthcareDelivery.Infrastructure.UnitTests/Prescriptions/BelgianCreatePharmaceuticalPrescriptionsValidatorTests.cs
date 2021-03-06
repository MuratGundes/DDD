﻿using Xunit;
using FluentValidation;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace DDD.HealthcareDelivery.Infrastructure.Prescriptions
{
    using Application.Prescriptions;

    [Trait("Category", "Unit")]
    public class BelgianCreatePharmaceuticalPrescriptionsValidatorTests
    {

        #region Methods

        public static IEnumerable<object[]> CommandsWithMedicationsEmpty()
        {
            yield return new object[]
            {
                CommandWithMedications(null)
            };
            yield return new object[]
            {
                CommandWithMedications(new PrescribedMedicationDescriptor[] { })
            };
        }

        public static IEnumerable<object[]> CommandsWithPrescriptionsEmpty()
        {
            yield return new object[]
            {
                CommandWithPrescriptions(null)
            };
            yield return new object[]
            {
                CommandWithPrescriptions(new PharmaceuticalPrescriptionDescriptor[] { })
            };
        }

        public static BelgianCreatePharmaceuticalPrescriptionsValidator CreateValidator()
        {
            return new BelgianCreatePharmaceuticalPrescriptionsValidator();
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("11111")]
        [InlineData("111111a")]
        public void Validate_WhenMedicationCodeInvalid_ReturnsExpectedFailure(string medicationCode)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedications
            (
                new PrescribedMedicationDescriptor { Code = medicationCode }
            );
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().Contain(f => f.ErrorCode == "MedicationCodeInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("0000000")]
        [InlineData("0123456")]
        public void Validate_WhenMedicationCodeValid_ReturnsNoSpecificFailure(string medicationCode)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedications
            (
                new PrescribedMedicationDescriptor { Code = medicationCode }
            );
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "MedicationCodeInvalid");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void Validate_WhenHealthFacilityIdentifierInvalid_ReturnsExpectedFailure(int healthFacilityIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { HealthFacilityIdentifier = healthFacilityIdentifier };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "HealthFacilityIdentifierInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Validate_WhenHealthFacilityIdentifierValid_ReturnsNoSpecificFailure(int healthFacilityIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { HealthFacilityIdentifier = healthFacilityIdentifier };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "HealthFacilityIdentifierInvalid");
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("11111")]
        [InlineData("1111111a")]
        public void Validate_WhenHealthFacilityLicenseNumberInvalid_ReturnsExpectedFailure(string healthFacilityLicenseNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { HealthFacilityLicenseNumber = healthFacilityLicenseNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().Contain(f => f.ErrorCode == "HealthFacilityLicenseNumberInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("11111111")]
        [InlineData("01234567")]
        public void Validate_WhenHealthFacilityLicenseNumberValid_ReturnsNoSpecificFailure(string healthFacilityLicenseNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { HealthFacilityLicenseNumber = healthFacilityLicenseNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "HealthFacilityLicenseNumberInvalid");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenHealthFacilityNameEmpty_ReturnsExpectedFailure(string healthFacilityName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { HealthFacilityName = healthFacilityName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "HealthFacilityNameEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("Healthcenter Donald Duck")]
        [InlineData("Centre ophtalmo")]
        public void Validate_WhenHealthFacilityNameNotEmpty_ReturnsNoSpecificFailure(string healthFacilityName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { HealthFacilityName = healthFacilityName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "HealthFacilityNameEmpty");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenLanguageCodeEmpty_ReturnsExpectedFailure(string languageCode)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { LanguageCode = languageCode };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "LanguageCodeEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("ffff")]
        [InlineData("12")]
        [InlineData("1a")]
        public void Validate_WhenLanguageCodeInvalid_ReturnsExpectedFailure(string languageCode)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { LanguageCode = languageCode };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().Contain(f => f.ErrorCode == "LanguageCodeInvalid" && f.Severity == Severity.Error);
        }

        [Fact]
        public void Validate_WhenLanguageCodeNotEmpty_ReturnsNoSpecificFailure()
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { LanguageCode = "BE" };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "LanguageCodeEmpty");
        }

        [Theory]
        [InlineData("BE")]
        [InlineData("be")]
        [InlineData("NL")]
        public void Validate_WhenLanguageCodeValid_ReturnsNoSpecificFailure(string languageCode)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { LanguageCode = languageCode };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "LanguageCodeInvalid");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenMedicationNameOrDescriptionEmpty_ReturnsExpectedFailure(string nameOrDescription)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedications
            (
                new PrescribedMedicationDescriptor { NameOrDescription = nameOrDescription }
            );
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "MedicationNameOrDescriptionEmpty" && f.Severity == Severity.Error);
        }

        [Fact]
        public void Validate_WhenMedicationNotNull_ReturnsNoSpecificFailure()
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedicationCount(2);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "MedicationNull");
        }

        [Fact]
        public void Validate_WhenMedicationNull_ReturnsExpectedFailure()
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedications((PrescribedMedicationDescriptor)null);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "MedicationNull" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(15)]
        public void Validate_WhenMedicationsCountInvalid_ReturnsExpectedFailure(int medicationCount)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedicationCount(medicationCount);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "MedicationsCountInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(9)]
        [InlineData(10)]
        public void Validate_WhenMedicationsCountValid_ReturnsNoSpecificFailure(int medicationCount)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedicationCount(medicationCount);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "MedicationsCountInvalid");
        }

        [Theory]
        [MemberData(nameof(CommandsWithMedicationsEmpty))]
        public void Validate_WhenMedicationsEmpty_ReturnsExpectedFailure(CreatePharmaceuticalPrescriptions command)
        {
            // Arrange
            var validator = CreateValidator();
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "MedicationsEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void Validate_WhenMedicationsNotEmpty_ReturnsNoSpecificFailure(int medicationCount)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedicationCount(medicationCount);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "MedicationsEmpty");
        }

        [Fact]
        public void Validate_WhenNameOrDescriptionNotEmpty_ReturnsNoSpecificFailure()
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithMedications
            (
                new PrescribedMedicationDescriptor { NameOrDescription = "Dualkopt Coll. 10 ml" }
            );
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "NameOrDescriptionEmpty");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenPatientFirstNameEmpty_ReturnsExpectedFailure(string patientFirstName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientFirstName = patientFirstName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PatientFirstNameEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("Olivier")]
        [InlineData("Luc")]
        public void Validate_WhenPatientFirstNameNotEmpty_ReturnsNoSpecificFailure(string patientFirstName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientFirstName = patientFirstName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PatientFirstNameEmpty");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void Validate_WhenPatientIdentifierInvalid_ReturnsExpectedFailure(int patientIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientIdentifier = patientIdentifier };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PatientIdentifierInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Validate_WhenPatientIdentifierValid_ReturnsNoSpecificFailure(int patientIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientIdentifier = patientIdentifier };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PatientIdentifierInvalid");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenPatientLastNameEmpty_ReturnsExpectedFailure(string patientLastName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientLastName = patientLastName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PatientLastNameEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("Dupont")]
        [InlineData("Jacobs")]
        public void Validate_WhenPatientLastNameNotEmpty_ReturnsNoSpecificFailure(string patientLastName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientLastName = patientLastName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PatientLastNameEmpty");
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("11111")]
        [InlineData("1111111111a")]
        public void Validate_WhenPatientSocialSecurityNumberInvalid_ReturnsExpectedFailure(string patientSocialSecurityNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientSocialSecurityNumber = patientSocialSecurityNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().Contain(f => f.ErrorCode == "PatientSocialSecurityNumberInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("11111111111")]
        [InlineData("01034567890")]
        public void Validate_WhenPatientSocialSecurityNumberValid_ReturnsNoSpecificFailure(string patientSocialSecurityNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PatientSocialSecurityNumber = patientSocialSecurityNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PatientSocialSecurityNumberInvalid");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenPrescriberFirstNameEmpty_ReturnsExpectedFailure(string prescriberFirstName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberFirstName = prescriberFirstName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PrescriberFirstNameEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("Olivier")]
        [InlineData("Luc")]
        public void Validate_WhenPrescriberFirstNameNotEmpty_ReturnsNoSpecificFailure(string prescriberFirstName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberFirstName = prescriberFirstName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriberFirstNameEmpty");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void Validate_WhenPrescriberIdentifierInvalid_ReturnsExpectedFailure(int prescriberIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberIdentifier = prescriberIdentifier };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PrescriberIdentifierInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Validate_WhenPrescriberIdentifierValid_ReturnsNoSpecificFailure(int prescriberIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberIdentifier = prescriberIdentifier };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriberIdentifierInvalid");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenPrescriberLastNameEmpty_ReturnsExpectedFailure(string prescriberLastName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberLastName = prescriberLastName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PrescriberLastNameEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("Dupont")]
        [InlineData("Jacobs")]
        public void Validate_WhenPrescriberLastNameNotEmpty_ReturnsNoSpecificFailure(string prescriberLastName)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberLastName = prescriberLastName };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriberLastNameEmpty");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Validate_WhenPrescriberLicenseNumberEmpty_ReturnsExpectedFailure(string prescriberLicenseNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberLicenseNumber = prescriberLicenseNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PrescriberLicenseNumberEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("11111")]
        [InlineData("1111111111a")]
        public void Validate_WhenPrescriberLicenseNumberInvalid_ReturnsExpectedFailure(string prescriberLicenseNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberLicenseNumber = prescriberLicenseNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().Contain(f => f.ErrorCode == "PrescriberLicenseNumberInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("11111")]
        public void Validate_WhenPrescriberLicenseNumberNotEmpty_ReturnsNoSpecificFailure(string prescriberLicenseNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberLicenseNumber = prescriberLicenseNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriberLicenseNumberEmpty");
        }

        [Theory]
        [InlineData("11111111111")]
        [InlineData("01034567890")]
        public void Validate_WhenPrescriberLicenseNumberValid_ReturnsNoSpecificFailure(string prescriberLicenseNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberLicenseNumber = prescriberLicenseNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriberLicenseNumberInvalid");
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("11111")]
        [InlineData("1111111111a")]
        public void Validate_WhenPrescriberSocialSecurityNumberInvalid_ReturnsExpectedFailure(string prescriberSocialSecurityNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberSocialSecurityNumber = prescriberSocialSecurityNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().Contain(f => f.ErrorCode == "PrescriberSocialSecurityNumberInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData("11111111111")]
        [InlineData("01034567890")]
        public void Validate_WhenPrescriberSocialSecurityNumberValid_ReturnsNoSpecificFailure(string prescriberSocialSecurityNumber)
        {
            // Arrange
            var validator = CreateValidator();
            var command = new CreatePharmaceuticalPrescriptions { PrescriberSocialSecurityNumber = prescriberSocialSecurityNumber };
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriberSocialSecurityNumberInvalid");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-5)]
        public void Validate_WhenPrescriptionIdentifierInvalid_ReturnsExpectedFailure(int prescriptionIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithPrescriptions
            (
                new PharmaceuticalPrescriptionDescriptor { PrescriptionIdentifier = prescriptionIdentifier }
            );
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PrescriptionIdentifierInvalid" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Validate_WhenPrescriptionIdentifierValid_ReturnsNoSpecificFailure(int prescriptionIdentifier)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithPrescriptions
            (
                new PharmaceuticalPrescriptionDescriptor { PrescriptionIdentifier = prescriptionIdentifier }
            );
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriptionIdentifierInvalid");
        }

        [Fact]
        public void Validate_WhenPrescriptionNotNull_ReturnsNoSpecificFailure()
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithPrescriptionCount(2);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriptionNull");
        }

        [Fact]
        public void Validate_WhenPrescriptionNull_ReturnsExpectedFailure()
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithPrescriptions((PharmaceuticalPrescriptionDescriptor)null);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PrescriptionNull" && f.Severity == Severity.Error);
        }

        [Theory]
        [MemberData(nameof(CommandsWithPrescriptionsEmpty))]
        public void Validate_WhenPrescriptionsEmpty_ReturnsExpectedFailure(CreatePharmaceuticalPrescriptions command)
        {
            // Arrange
            var validator = CreateValidator();
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().ContainSingle(f => f.ErrorCode == "PrescriptionsEmpty" && f.Severity == Severity.Error);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        public void Validate_WhenPrescriptionsNotEmpty_ReturnsNoSpecificFailure(int prescriptionCount)
        {
            // Arrange
            var validator = CreateValidator();
            var command = CommandWithPrescriptionCount(prescriptionCount);
            // Act
            var results = validator.Validate(command);
            // Assert
            results.Errors.Should().NotContain(f => f.ErrorCode == "PrescriptionsEmpty");
        }
        private static CreatePharmaceuticalPrescriptions CommandWithMedicationCount(int medicationCount)
        {
            var command = CommandWithPrescriptionCount(1);
            var prescription = command.Prescriptions.First();
            for (var i = 0; i < medicationCount; i++)
                prescription.Medications.Add(new PrescribedMedicationDescriptor());
            return command;
        }

        private static CreatePharmaceuticalPrescriptions CommandWithMedications(params PrescribedMedicationDescriptor[] medications)
        {
            var command = CommandWithPrescriptionCount(1);
            var prescription = command.Prescriptions.First();
            prescription.Medications = medications;
            return command;
        }

        private static CreatePharmaceuticalPrescriptions CommandWithPrescriptionCount(int prescriptionCount)
        {
            var command = new CreatePharmaceuticalPrescriptions();
            for (var i = 0; i < prescriptionCount; i++)
                command.Prescriptions.Add(new PharmaceuticalPrescriptionDescriptor());
            return command;
        }

        private static CreatePharmaceuticalPrescriptions CommandWithPrescriptions(params PharmaceuticalPrescriptionDescriptor[] prescriptions)
        {
            return new CreatePharmaceuticalPrescriptions
            {
                Prescriptions = prescriptions
            };
        }

        #endregion Methods

    }
}
