import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BalancingProcessComponent } from './balancing-process.component';

describe('BalancingProcessComponent', () => {
  let component: BalancingProcessComponent;
  let fixture: ComponentFixture<BalancingProcessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BalancingProcessComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BalancingProcessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
