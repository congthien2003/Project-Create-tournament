import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormFormatTypeComponent } from './form-format-type.component';

describe('FormFormatTypeComponent', () => {
  let component: FormFormatTypeComponent;
  let fixture: ComponentFixture<FormFormatTypeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FormFormatTypeComponent]
    });
    fixture = TestBed.createComponent(FormFormatTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
