/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MoradorComponent } from './morador.component';

describe('MoradorComponent', () => {
  let component: MoradorComponent;
  let fixture: ComponentFixture<MoradorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MoradorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MoradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
