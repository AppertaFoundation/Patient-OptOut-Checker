import { TestBed } from '@angular/core/testing';

import { PatientOptOutService } from './patient-opt-out.service';

describe('PatientOptOutService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: PatientOptOutService = TestBed.get(PatientOptOutService);
    expect(service).toBeTruthy();
  });
});
