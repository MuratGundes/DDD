SELECT   PrescriptionId AS Identifier,
         CASE Status
			WHEN 'CRT' THEN 1
			WHEN 'INP' THEN 2
			WHEN 'DLV' THEN 3
			WHEN 'RVK' THEN 4
			WHEN 'EXP' THEN 5
			WHEN 'ARC' THEN 6
		 END AS Status,
         CreatedOn,
         DelivrableAt,
		 PrescriberDisplayName
FROM     Prescription
WHERE    PrescriptionType = 'PHARM'
AND      PatientId = @PatientId
ORDER BY CreatedOn, PrescriptionId