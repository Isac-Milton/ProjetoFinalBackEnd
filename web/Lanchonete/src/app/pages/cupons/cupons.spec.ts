import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Cupons } from './cupons';

describe('Cupons', () => {
  let component: Cupons;
  let fixture: ComponentFixture<Cupons>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Cupons],
    }).compileComponents();

    fixture = TestBed.createComponent(Cupons);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
